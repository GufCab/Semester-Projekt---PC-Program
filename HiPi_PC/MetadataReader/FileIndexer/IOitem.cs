﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataReader.FileIndexer
{
    /// <summary>
    /// This class contains the information about each IO item.
    /// </summary>
    class IOitem
    {
        public string Tag { get; private set; }
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public string Extention { get; private set; }
        public string FilePath { get; private set; }

        /// <summary>
        /// An overloaded constructor which looks up the informations from a file given in the type System.IO.FileInfo
        /// </summary>
        /// <param name="fileInfo">The file that needs to be looked up</param>
        public IOitem(System.IO.FileInfo fileInfo)
        {
            Name = fileInfo.Name;
            FullName = fileInfo.FullName;
            Tag = "file";
            Extention = fileInfo.Extension;
            FilePath = fileInfo.DirectoryName;
        }

        /// <summary>
        /// An overloaded constructor which looks up the informations from a folder given in the type System.IO.DirectoryInfo
        /// </summary>
        /// <param name="directoryInfo">The folder that need's to be looked up</param>
        public IOitem(System.IO.DirectoryInfo directoryInfo)
        {
            Name = directoryInfo.Name;
            FullName = directoryInfo.FullName;
            Tag = "folder";
        }
    }
}
