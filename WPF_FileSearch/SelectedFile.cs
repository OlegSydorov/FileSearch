using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_FileSearch
{
    public class SelectedFile
    {
       public string FileIcon { get; set; } 
        public string Name { get; set; }
        public string Extension { get; set; }
         public long Size { get; set; }
        public string Path { get; set; }       
        public DateTime LastChanged { get; set; }
        
    }
}
