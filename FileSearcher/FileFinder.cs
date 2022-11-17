using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;
using static WPF_FileSearch.MainWindow;

namespace FileSearcher
{
    //class DetectedFile
    //{
    //    string name;
    //    string extension;
    //    string path;
    //    long size;
    //    Icon fileIcon;
    //    DateTime lastChanged;

    //    public string Name { get { return name; } set { name = value; } }
    //    public string Extension { get { return extension; } set { extension = value; } }
    //    public string Path { get { return path; } set { path = value; } }
    //    public long Size { get { return size; } set { size = value; } }
    //    public Icon FileIcon { get { return fileIcon; } set { fileIcon = value; } }
    //    public DateTime LastChanged { get { return lastChanged; } set { lastChanged = value; } }

    //    public DetectedFile(string na, string ext, string pa, long si, DateTime time, Icon ic)
    //    {
    //        name = na;
    //        extension = ext;
    //        path = pa;
    //        size = si;
    //        lastChanged = time;
    //        fileIcon = ic;
    //    }

    //}

    public class FileFinder
    {
        /*Thread t1=null;
        //DataGrid mainDataGrid;
        // Window MainWindow;
        MethodInfo dataUpdate;
        //ObservableCollection<DetectedFile> observableFileCollection;
        //CollectionViewSource fileCollection;
        //List<DetectedFile> fileList = new List<DetectedFile>();
        FileAddCallBack cb;

        public void Seek (string[] data)
        {
            // MessageBox.Show(path + " " + mask);
            DirectoryInfo d1 = new DirectoryInfo($"{data[0]}");
            if (d1.Exists == true)
            {
                try
                {
                    FileInfo[] files = d1.GetFiles(data[1]);
                    foreach (FileInfo current in files)
                    {

                        Icon ic = Icon.ExtractAssociatedIcon(current.FullName);
                        //DetectedFile file = new DetectedFile(current.Name, current.Extension, current.FullName, current.Length, current.LastWriteTime, ic);
                        //Thread.Sleep(100);
                        

                        //object[] arg = new object[6];
                        //arg[0] = current.Name;
                        //arg[1] = current.Extension;
                        //arg[2] = current.FullName;
                        //arg[3] = current.Length;
                        //arg[4] = current.LastWriteTime;
                        //arg[5] = ic;

                        //Object obj = Activator.CreateInstance(dataUpdate.GetType(), arg);

                        ////object[] arg = new object[6];
                        ////arg[0] = current.Name;
                        ////arg[1] = current.Extension;
                        ////arg[2] = current.FullName;
                        ////arg[3] = current.Length;
                        ////arg[4] = current.LastWriteTime;
                        ////arg[5] = ic;


                        //dataUpdate.Invoke(obj, arg);

                        //fileList.Add(file);

                        cb(current.Name, current.Extension, current.FullName, current.Length, current.LastWriteTime, ic);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    DirectoryInfo[] dirs = d1.GetDirectories();
                    foreach (DirectoryInfo current in dirs)
                    {
                        //Icon ic = Icon.ExtractAssociatedIcon(current.FullName);
                        // DetectedFile file = new DetectedFile(current.Name, "FOLDER", current.FullName, 0, current.LastWriteTime, null);
                        // fileList.Add(file);

                        // Thread.Sleep(100);

                        //Object obj = Activator.CreateInstance(dataUpdate.GetType());

                        //object[] arg = new object[6];
                        //arg[0] = current.Name;
                        //arg[1] = "FOLDER";
                        //arg[2] = current.FullName;
                        //arg[3] = 0;
                        //arg[4] = current.LastWriteTime;
                        //arg[5] = null;
                        //dataUpdate.Invoke(obj, arg);

                        cb(current.Name, "FOLDER", current.FullName, 0, current.LastWriteTime, null);

                        string[] dataNew = new string[2];
                        dataNew[0] = data[0] + @"\" + current.Name;
                        dataNew[1] = data[1];

                        Seek(dataNew);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else Console.WriteLine("Incorrect path entered!");
        }

        public delegate void FileAddCallBack(string name, string extension, string path, long size, DateTime lastWrite, Icon icon);
        public void ThreadStart(string path, string mask, FileAddCallBack callback)
        {
            //mainDataGrid = new DataGrid();
            //mainDataGrid = dGrid;

            //this.MainWindow = MainWindow;

            //this.dataUpdate = DataUpdate;

            // ParameterizedThreadStart pts = new ParameterizedThreadStart(Seek);

            cb = callback;
            try
            {
                if (t1 == null)
                {
                    //t1 = new Thread(() => Seek(path, mask));

                    //t1.IsBackground = true;
                    ////object[] arg = new object[2];
                    ////arg[0] = path;
                    ////arg[1] = mask;
                    //t1.Start();
                    //FileFinder ff = new FileFinder();
                    //ParameterizedThreadStart p1 = new ParameterizedThreadStart(Seek);
                   // object[] arg = new object[1];
                   // arg[0] = new string[2] { path, mask };

                    string[] s=new string[2] { path, mask };

                    t1 = new Thread(() => Seek(s));
                    //t1.IsBackground = true;
                    MessageBox.Show(t1.Name+"//"+t1.ToString());
                    //arg[1] = mask;
                    t1.Start();
                    MessageBox.Show(t1.Name + "//" + t1.ToString()+"//"+ t1.ThreadState.ToString());
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Pause()
        {
            t1.Suspend();
        }

        public void Resume()
        {
            t1.Resume();

        }

        public void Stop()
        {
            t1.Abort();
        }

        //delegate void UpdateDelegate();
        //private void DataUpdate()
        //{
        //    if (!Application.Current.Dispatcher.CheckAccess())
        //    {
        //        UpdateDelegate update = new UpdateDelegate (DataUpdate);
        //        Application.Current.Dispatcher.Invoke(update, new object[] {null });
        //    }
        //    else
        //    {
        //        if (fileList != null)
        //        {
        //            if (fileList.Count > 0)
        //            {
        //                observableFileCollection = new ObservableCollection<DetectedFile>(fileList);
        //                fileCollection = new CollectionViewSource() { Source = observableFileCollection };
        //                mainDataGrid.ItemsSource = null;
        //                mainDataGrid.ItemsSource = fileCollection.View;
        //            }
        //        }
        //    }           
        //}*/

        public void Seek(string path, string mask, FileAddCallBack callback)
        {
            DirectoryInfo d1 = new DirectoryInfo($"{path}");
            if (d1.Exists == true)
            {
                try
                {
                    FileInfo[] files = d1.GetFiles(mask);
                    foreach (FileInfo current in files)
                    {
                        Icon ic = Icon.ExtractAssociatedIcon(current.FullName);                       
                        callback(current.Name, current.Extension, current.FullName, current.Length, current.LastWriteTime, ic);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    DirectoryInfo[] dirs = d1.GetDirectories();
                    foreach (DirectoryInfo current in dirs)
                    {
                        if (mask == "*.*") callback(current.Name, "FOLDER", current.FullName, 0, current.LastWriteTime, null);
                        Seek(path + @"\" + current.Name, mask, callback);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            else Console.WriteLine("Incorrect path entered!");
        }
    }
}
