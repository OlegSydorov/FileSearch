using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using ThreadState = System.Threading.ThreadState;

namespace WPF_FileSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool maskFlag;
        string folderPath;
        string mask;
        PlugInManager piManager;
        ObservableCollection<DetectedFile> observableFileCollection;
        CollectionViewSource fileCollection;
        static List<DetectedFile> fileList;
        Dictionary<string, string> icons;
        string threadStateFlag;
        long fileCount;

        Thread t1=null;

        public MainWindow()
        {
            InitializeComponent();

            piManager = new PlugInManager(this);
            fileList = new List<DetectedFile>();
            threadStateFlag = null;
            folderPath = null;
            mask = null;
            maskFlag = false;
            //String baseDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory;
            //string assemblyPath = baseDirectoryPath + "FileSearcher.dll";
            fileCount = 0;
            CollectIcons();
        }

        private void CollectIcons()
        {
            icons = new Dictionary<string, string>();
            String baseDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory;
            string iconsPath = baseDirectoryPath + "\\Icons";
            try
            {
                DirectoryInfo d1 = new DirectoryInfo($"{iconsPath}");
                FileInfo[] files = d1.GetFiles();
                foreach (FileInfo current in files)
                {
                    if (current.Extension == ".ICO")
                    {
                        string iconName = current.Name.Substring(0, current.Name.Length-4);
                        icons.Add(iconName, current.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbMouseDown(object sender, MouseEventArgs e)
        {
            if (maskFlag == false)
            {
                maskFlag = true;
                tb.Text = "";
                
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            if (t1.ThreadState == ThreadState.Suspended)
            {
                t1.Resume();
                t1.Abort();
            }
            else if (t1.ThreadState == ThreadState.Running)
            {
                t1.Abort();
            }
                this.Close();
        }

        private void skinButton_Click(object sender, RoutedEventArgs e)
        {
           skins.Visibility=Visibility.Visible;
           skins.ItemsSource = piManager.GetPlugInsNames();
        }

        private void skinsSelectionChanged(object sender, RoutedEventArgs e)
        {
            skins.Visibility = Visibility.Hidden;
            piManager.ActivatePlugIn(skins.SelectedItem.ToString());
        }

        private void folderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.InitialDirectory = @"c:\";
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                folderPath = null;
                folderPath = dialog.FileName;
                tbFinal.Text = "FOLDER: " + folderPath;
            }
        }

        private void fileButton_Click(object sender, RoutedEventArgs e)
        {
            tb.Visibility = Visibility.Visible;
            tb.Text = "Enter filename, part of file name or file extension";
            maskFlag = false;
        }

        

        public delegate void FileAddCallBack(string name, string extension, string path, long size, DateTime lastWrite, Icon icon);

        public delegate void FileFindCallBack(string name, string extension, string path, long size, DateTime lastWrite, Icon icon);
        public void FileAdd(string name, string extension, string path, long size, DateTime lastWrite, Icon icon)
        {
            if (!Dispatcher.CheckAccess())  // запущена ли эта функция не в основном потоке?
            {
                FileFindCallBack filefound = new FileFindCallBack(FileAdd);
                Application.Current.Dispatcher.Invoke(filefound, name, extension, path, size, lastWrite, icon);
            }

           
            else
            {
                string iconPath=null;
                String baseDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory;
                if (extension == "FOLDER")
                    iconPath = baseDirectoryPath + "\\Icons\\" + "CLSDFOLD.ICO";
                else
                {
                    if (icons.ContainsKey(extension)) iconPath = baseDirectoryPath + "\\Icons\\" + icons[extension];
                    else
                    {
                        try
                        {
                            string targetPath = baseDirectoryPath + "\\Icons\\" + extension + ".ICO";
                            FileStream fs = new FileStream(targetPath, FileMode.Create);
                            icon.Save(fs);
                            fs.Close();
                            icons.Add(extension, extension + ".ICO");
                            iconPath = targetPath;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                }

                fileList.Add(new DetectedFile(name, extension, path, size, lastWrite, iconPath));
                fileCount++;
                if (pBar.Value < 100)
                {
                    pBar.Value++;
                }
                else pBar.Value = 0;
                pBar.ToolTip = fileList.Count;
                DataUpdate();
            }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (folderPath.Length > 0)
            {
                if (tb.Text.Length > 0 && tb.Text != "Enter filename, part of file name or file extension")
                {
                    fileCount = 0;
                    tb.Visibility = Visibility.Hidden;
                    tbFinal.Text = "";
                    tbFinal.Text = "FOLDER: " + folderPath + " // FILE: " + tb.Text;

                    pBar.Value = 0;
                    mask = tb.Text;
                    fileList.Clear();
                    mainDataGrid.ItemsSource = null;
                    
                    String baseDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory;
                    string assemblyPath = baseDirectoryPath + "FileSearcher.dll";

                    Assembly assembly = null;
                    try
                    {
                        assembly = Assembly.LoadFrom(assemblyPath);

                        Type[] types = assembly.GetTypes();
                        foreach (Type t in types)
                        {
                            if (t.Name == "FileFinder")
                            {

                                MethodInfo[] mi = t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                                foreach (MethodInfo m in mi)
                                {
                                    if (m.Name == "Seek")
                                    {
                                        try
                                        {
                                            FileAddCallBack callback = new FileAddCallBack(FileAdd);

                                            Object obj = Activator.CreateInstance(t);

                                            object[] arg = new object[3];
                                            arg[0] = folderPath;
                                            arg[1] = mask;
                                            //arg[2] = this;
                                            arg[2] = callback;
                                            t1=new Thread(() => m.Invoke(obj, arg));
                                            t1.IsBackground = true;
                                            t1.Start();
                                            threadStateFlag = "running";
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else MessageBox.Show("File mask not entered!");
            }
            else MessageBox.Show("No folder selected!");
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (threadStateFlag == "running")
            {
                t1.Suspend();
                threadStateFlag = "suspended";
            }
            
        }

        private void resumeButton_Click(object sender, RoutedEventArgs e)
        {
            if (threadStateFlag == "suspended")
            {
                t1.Resume();
                threadStateFlag = "running";
            }
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            if (threadStateFlag == "running")
            {
                t1.Abort();
                threadStateFlag = "aborted";
            }
        }
        public void DataUpdate()//string name, string extension, string path,long size, DateTime lastWrite, Icon icon)
        {
            observableFileCollection = new ObservableCollection<DetectedFile>(fileList);
            fileCollection = new CollectionViewSource() { Source = observableFileCollection };
            mainDataGrid.ItemsSource = null;
            mainDataGrid.ItemsSource = fileCollection.View;
        }

        private void MainDataGrid_Click(object sender, MouseButtonEventArgs e)
        {
            if (mainDataGrid.SelectedItems.Count > 0)
            {
                DetectedFile selectedData = mainDataGrid.SelectedItem as DetectedFile;
                if (selectedData != null)
                {
                    if (selectedData.Extension == ".exe")
                    {
                        MessageBox.Show(selectedData.Name);
                        Process.Start(selectedData.Path);
                    }
                }
                
            }
        }


    }
}
