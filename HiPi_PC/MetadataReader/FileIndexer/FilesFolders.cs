using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataReader.FileIndexer
{
    /// <summary>
    /// FilesFolder findes the different files and folders, in the given folder
    /// </summary>
    class FilesFolders
    {
        public List<IOitem> IndexContainer { get; private set; }
        private readonly string _path;

        //System.IO.DirectoryInfo:
        private System.IO.DirectoryInfo _directory;

        /// <summary>
        /// Constructor taking a path as parameter, runs, and then sets the IndexContainer list as result
        /// which contains all elements in the path
        /// </summary>
        /// <param name="path"></param>
        public FilesFolders(string path)
        {
            _path = path;
            IndexContainer = new List<IOitem>();
            SetupDirectoryPath();
            IndexFilesAndFolders();
        }

        /// <summary>
        /// Sets the _directory property, as a System.IO.DirectoryInfo.
        /// This is sat according to the path given in the constructor
        /// </summary>
        private void SetupDirectoryPath()
        {
            if (_path == null)
                throw new Exception("path in FilesFolders is null\n");
 
            _directory = new DirectoryInfo(_path);
        }

        /// <summary>
        /// This runs the functions SetFiles and SetFolders
        /// </summary>
        private void IndexFilesAndFolders()
        {
            SetFiles();
            SetFolders();
        }

        /// <summary>
        /// This add's all the folders in the given path to the IndexContainer, as IOitem objects
        /// </summary>
        private void SetFolders()
        {
            foreach (DirectoryInfo directoryInfo in _directory.GetDirectories())
            {
                IndexContainer.Add(new IOitem(directoryInfo));
            }
        }

        /// <summary>
        /// This add's all the files in the given path to the IndexContainer, as IOitem objects
        /// </summary>
        private void SetFiles()
        {
            foreach (FileInfo fileInfo in _directory.GetFiles("*.*"))
            {
                IndexContainer.Add(new IOitem(fileInfo));
            }
        }
    }
}
