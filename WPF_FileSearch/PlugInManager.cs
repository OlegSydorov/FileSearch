using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_FileSearch
{
    class PlugInItem
    {
        public String Name { get; set; }
        public Type Type { get; set; }
        public MethodInfo Method { get; set; }
    }

    public class PlugInManager
    {
        public Window MainWindow { get; }
        public PlugInManager(Window mainWindow)
        {
            MainWindow = mainWindow;
        }
        
        public MethodInfo CheckPlugIn(String fullName, out Type plugInType)
        {
            plugInType = null;

            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFrom(fullName);
            }
            catch (Exception ex)
            {
                return null;
            }

           
            if (assembly == null)
                return null;

            foreach (Type currentType in assembly.GetTypes())
            {
                if (currentType.Name == "PlugIn")
                {
                    plugInType = currentType;
                    break;
                }
            }

           if (plugInType == null || !plugInType.IsClass)
                return null;
     
            MethodInfo plugInMethod = null;
            foreach (MethodInfo currentMethod in plugInType.GetMethods())
            {
                if (currentMethod.Name == "ChangeSkin")
                {
                    plugInMethod = currentMethod;
                    break;
                }
            }
            
            if (plugInMethod == null || !plugInMethod.IsPublic)
                return null;

            return plugInMethod;
        }
                
        Dictionary<String, PlugInItem> plugIns = new Dictionary<string, PlugInItem>();
        public List<String> GetPlugInsNames()
        {
            List<String> names = new List<string>();
            plugIns.Clear();
            String baseDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo dinfo = new DirectoryInfo(baseDirectoryPath + @"Plug-ins");
            FileInfo[] files = dinfo.GetFiles("*.dll");
            foreach (FileInfo currentFile in files)
            {
                Type plugInType = null;
                MethodInfo plugInMethod = CheckPlugIn(currentFile.FullName, out plugInType);

                if (plugInMethod != null)
                {
                    names.Add(currentFile.Name);
                    plugIns.Add(currentFile.Name, new PlugInItem() { Name = currentFile.Name, Method = plugInMethod, Type = plugInType });
                }
            }

            return names;
        }

        public void ActivatePlugIn(String name)
        {
            PlugInItem item = plugIns[name];

            if (item != null)
            {
                Object obj = Activator.CreateInstance(item.Type);
                object[] arg = new object[1];
                arg[0] = MainWindow;
                item.Method.Invoke(obj, arg);
            }
        }
    }
}
