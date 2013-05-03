using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaReader.MetadataReader;
using Shell32;

namespace MetaReader.FileIndexer
{
    class FileIndexer : IFileIndexer
    {
        //Gets the file lokation
        //returns the metadata as List<Objects>
        public string Folderpath { get; private set; }

        //setup
        public List<string> FolderItemList { get; private set; }
        //private int itemcount;
        private IMetadataReader metadata;
        private List<IMetadataReader> _metaList;

        //setup shell32.folderpath / objects
        private readonly Shell32.Shell _shell = new Shell32.Shell(); //consider making this a singleton
        private Shell32.Folder _objFolder;

        public FileIndexer(string folderpath)
        {
            Folderpath = folderpath;
            Setup();
        }

        public bool SetIndexPath(string folderpath)
        {
            Folderpath = folderpath;
            //if folderpath exist, return true;
            throw new NotImplementedException();
        }

        private void Setup()
        {
            _objFolder = _shell.NameSpace(Folderpath);
            FolderItemList = new List<string>();
            _metaList = new List<IMetadataReader>();
            
        }

        public List<string> GetItems()
        {
            foreach (Shell32.FolderItem2 folderItem in _objFolder.Items())
            {
                FolderItemList.Add(folderItem.Name);
            }
            return FolderItemList;
        }

        public List<IMetadataReader> GetMetaData()
        {
            //GetItems();
            //itemcount = FolderItemList.Count;
            int i = 0;

            foreach (Shell32.FolderItem2 item2 in _objFolder.Items())
            {
                metadata = new MetadataReader.MetadataReader(i, Folderpath);
                _metaList.Add(metadata);
                i++;
            }
            return _metaList;
        }

    }
}
