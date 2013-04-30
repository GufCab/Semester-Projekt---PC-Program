using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaReader.FileIndexer
{
    class FilesFolders
    {
        public List<IOitem> IndexContainer { get; private set; }
        private readonly string _path;

        //System.IO.DirectoryInfo:
        private System.IO.DirectoryInfo _directory;

        public FilesFolders(string path)
        {
            _path = path;
            IndexContainer = new List<IOitem>();
            SetupDirectoryPath();
            IndexFilesAndFolders();
        }

        private void SetupDirectoryPath()
        {
            _directory = new DirectoryInfo(_path);
            
        }

        private void IndexFilesAndFolders()
        {
            SetFiles();
            SetFolders();
        }

        private void SetFolders()
        {
            foreach (DirectoryInfo directoryInfo in _directory.GetDirectories())
            {
                IndexContainer.Add(new IOitem(directoryInfo));
            }
        }

        private void SetFiles()
        {
            foreach (FileInfo fileInfo in _directory.GetFiles("*.*"))
            {
                IndexContainer.Add(new IOitem(fileInfo));
            }
        }
    }
}
