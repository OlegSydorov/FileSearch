using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_FileSearch
{
    class DetectedFile
    {
        string name;
        string extension;
        string path;
        long size;
        string fileIcon;
        DateTime lastChanged;

        public string Name { get { return name; } set { name = value; } }
        public string Extension { get { return extension; } set { extension = value; } }
        public string Path { get { return path; } set { path = value; } }
        public long Size { get { return size; } set { size = value; } }
        public string FileIcon { get { return fileIcon; } set { fileIcon = value; } }
        public DateTime LastChanged { get { return lastChanged; } set { lastChanged = value; } }

        public DetectedFile(string na, string ext, string pa, long si, DateTime time, string iconPath)
        {
            name = na;
            extension = ext;
            path = pa;
            size = si;
            lastChanged = time;
            fileIcon = iconPath;
        }

    }
}
