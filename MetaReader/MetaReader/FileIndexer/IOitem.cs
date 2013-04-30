using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaReader.FileIndexer
{
    class IOitem
    {
        public string Tag { get; private set; }
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public string Extention { get; private set; }
        public string FilePath { get; private set; }

        public IOitem(System.IO.FileInfo fileInfo)
        {
            Name = fileInfo.Name;
            FullName = fileInfo.FullName;
            Tag = "file";
            Extention = fileInfo.Extension;
            FilePath = fileInfo.DirectoryName;
        }

        public IOitem(System.IO.DirectoryInfo directoryInfo)
        {
            Name = directoryInfo.Name;
            FullName = directoryInfo.FullName;
            Tag = "folder";
        }
    }
}
