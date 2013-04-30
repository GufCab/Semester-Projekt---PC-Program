using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaReader.MetadataReader;

namespace MetaReader.FileIndexer
{
    class FolderAndFileReader : IFileIndexer
    {

        //from FileIndexer
        //Gets the file lokation
        //returns the metadata as List<Objects>
        
        //Extention
        private ExtentionVerifier extentionVerifier = new ExtentionVerifier();

        // container to files and folder in given directory
        public List<string> FoldersAndFiles { get; private set; }
        
        public List<string> FolderItemList { get; private set; }

        //============================
        // Constructor
        //============================
        //Files and Folders - this to analyse
        private FilesFolders _folder;
        public string Folderpath { get; private set; }

        public FolderAndFileReader()
        {
            _metaList = new List<IMetadataReader>();
            FoldersAndFiles = new List<string>();
            FolderItemList = new List<string>();
            
            Folderpath = @"Biblioteker\Musik";
        }


        public void SetIndexPath(string folderpath)
        {
            Folderpath = folderpath;
            _folder = new FilesFolders(Folderpath);
            Runner();
        }

        private void Runner()
        {
            getMetaDataFromFile();
            foldersVers_One();
            GetAllMetaData(MusikList);
        }

        //============================
        // I don't know what it does
        //============================
        

        private void getMetaDataFromFile()
        {
            FilesFolders folder = new FilesFolders(Folderpath);
            foreach (IOitem ioitem in _folder.IndexContainer)
            {                
                if (extentionVerifier.TestExtention(ioitem.Extention))
                {

                }
            }
        }

        //============================
        // Find all folders in folder
        //============================

        //List<string> subfolders = new List<string>();
        private List<string> MusikList = new List<string>();
        private FilesFolders Container;
        private string testsubfolder;

        private void foldersVers_One()
        {
            Container = new FilesFolders(Folderpath);

            foreach (IOitem ioitem in Container.IndexContainer)
            {
                if (extentionVerifier.TestExtention(ioitem.Extention))
                {
                    MusikList.Add(ioitem.FullName);
                    Console.WriteLine(ioitem.FullName);
                }

                if (ioitem.Tag == "folder")
                {
                    //subfolders.Add(ioitem.FullName);
                    Console.WriteLine(ioitem.FullName);
                    SubFoldersLookup(ioitem.FullName);

                }
            }
            testsubfolder = null;
        }

        private void SubFoldersLookup(string folderpath)
        {
            if (folderpath == null) throw new ArgumentNullException("folderpath");

            if (folderpath == testsubfolder) throw new Exception("Recursive subfolder call");
            testsubfolder = folderpath;


            FilesFolders subContainer = new FilesFolders(folderpath);
            foreach (IOitem ioitem in subContainer.IndexContainer)
            {
                if (extentionVerifier.TestExtention(ioitem.Extention))
                {
                    MusikList.Add(ioitem.FullName);
                    Console.WriteLine(ioitem.FullName);
                }

                if (ioitem.Tag == "folder")
                {
                    Console.WriteLine(ioitem.FullName);
                    SubFoldersLookup(ioitem.FullName);

                }
            }
        }

        //============================
        // Not importent for funktionality
        //============================
        /*
        public void TempPrint()
        {
            FilesFolders folder = new FilesFolders(Folderpath);
            foreach (IOitem ioitem in folder.IndexContainer)
            {

                if (extentionVerifier.TestExtention(ioitem.Extention) || ioitem.Tag == "folder")
                {
                    Console.WriteLine(ioitem.Name);
                    Console.WriteLine(ioitem.Tag);
                    Console.WriteLine(ioitem.FullName);
                    Console.WriteLine(ioitem.Extention);
                }
            }
        }
        */
        //============================
        // Get Metadata
        //============================
        private IMetadataReader metadata;
        private List<IMetadataReader> _metaList;

        private void GetAllMetaData(IEnumerable<string> musikList)
        {
            if (musikList == null) throw new ArgumentNullException("musikList");

            var i = 0;

            foreach (string musiknumber in musikList)
            {
                GetFileMetaData(i, musiknumber);
                i++;
            }
        }

        private void GetFileMetaData(int indexNumber, string musiknumber)
        {
            metadata = new MetadataReader.MetadataReader(indexNumber, musiknumber);
            _metaList.Add(metadata);
        }

        //============================
        // This function returns the metadata.
        //============================

        public List<IMetadataReader> GetMetaData()
        {
            return _metaList;
        }
        
    }
}
