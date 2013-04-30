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
        //Files and Folders - this to analyse
        private FilesFolders _folder;

        //from FileIndexer
        //Gets the file lokation
        //returns the metadata as List<Objects>
        

        //private int itemcount;
        private IMetadataReader metadata;
        private List<IMetadataReader> _metaList;
        //ended from FileIndexer

        public string Folderpath { get; private set; }

        //Extention
        private ExtentionVerifier extentionVerifier = new ExtentionVerifier();

        // container to files and folder in given directory
        public List<string> FoldersAndFiles { get; private set; }
        
        public List<string> FolderItemList { get; private set; }

        //============================
        // Constructor
        //============================
        public FolderAndFileReader()
        {
            FoldersAndFiles = new List<string>();
            FolderItemList = new List<string>();
            

            Folderpath = @"Biblioteker\Musik";
        }


        public void SetIndexPath(string folderpath)
        {
            Folderpath = folderpath;
            Function();
        }

        //============================
        // I don't know what it does
        //============================
        private void Function()
        {
            _folder = new FilesFolders(Folderpath);
            getMetaDataFromFile();
        }

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
        // Get Metadata
        //============================

        private void SetMetaData()
        {

        }

        //============================
        // Find all folders in folder
        //============================

        List<string> subfolders = new List<string>();
        private FilesFolders Container;
        private string testsubfolder;

        public void foldersVers_One()
        {
            Container = new FilesFolders(Folderpath);

            foreach (IOitem ioitem in Container.IndexContainer)
            {
                if (extentionVerifier.TestExtention(ioitem.Extention))
                    Console.WriteLine(ioitem.FullName);

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
                    Console.WriteLine(ioitem.FullName);

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

        


        public List<IMetadataReader> GetMetaData()
        {
            throw new NotImplementedException();
        }
        
    }
}
