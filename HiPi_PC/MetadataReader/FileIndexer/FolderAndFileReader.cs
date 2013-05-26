using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetadataReader.Metadata;

namespace MetadataReader.FileIndexer
{
    /// <summary>
    /// The class FolderAndFileReader are the core class in the MetadataReader namespace.
    /// FolderAndFileReader looks up all files and folders, and subpaths in the given folder
    /// and returns the files Metadata in a list
    /// </summary>
    public class FolderAndFileReader : IFileIndexer
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

        /// <summary>
        /// Constructor creates the list's that are used, and set a default folderpath.
        /// </summary>
        public FolderAndFileReader()
        {
            _metaList = new List<IMetadataReader>();
            FoldersAndFiles = new List<string>();
            FolderItemList = new List<string>();
            
            Folderpath = @"Biblioteker\Musik";
        }

        /// <summary>
        /// Sets the folderpath, and starts the runner
        /// </summary>
        /// <param name="folderpath">The folder that are to indexed</param>
        public void SetIndexPath(string folderpath)
        {
            Folderpath = folderpath;
            _folder = new FilesFolders(Folderpath);
            Runner();
        }

        /// <summary>
        /// Runs the starts the folderIndexer, and after starts the GetAllMetaData function
        /// </summary>
        private void Runner()
        {
            foldersVers_One();
            GetAllMetaData(MusikList);
        }

        //============================
        // Find all folders in folder
        //============================

        //List<string> subfolders = new List<string>();
        private List<IOitem> MusikList = new List<IOitem>();
        private FilesFolders Container;
        private string testsubfolder;

        /// <summary>
        /// Look's up what in the given folder, and calls a subFoldersLookup with the folders in the first folder
        /// uses the extentionVerifier to ensure, that only musiknumbers are added to the List MusicList
        /// </summary>
        private void foldersVers_One()
        {
            Container = new FilesFolders(Folderpath);

            foreach (IOitem ioitem in Container.IndexContainer)
            {
                if (extentionVerifier.TestExtention(ioitem.Extention))
                {
                    MusikList.Add(ioitem);
                    //Console.WriteLine("added " + ioitem.FullName);
                }

                if (ioitem.Tag == "folder")
                {
                    //Console.WriteLine(ioitem.FullName);
                    SubFoldersLookup(ioitem.FullName);
                }
            }
            testsubfolder = null;
        }

        /// <summary>
        /// looks what in the given folder, and makes a recursive call to itself, with the folders that is in the given folder.
        /// To ensure that it doesn't make a endless recursive call, it check the foldername, to see that it is not the same as the last
        /// uses the extentionVerifier to ensure, that only musiknumbers are added to the List MusicList
        /// </summary>
        /// <param name="folderpath">The folder that are to be indexed.</param>
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
                    MusikList.Add(ioitem);
                    //Console.WriteLine("subfolder added " + ioitem.FullName);
                }

                if (ioitem.Tag == "folder")
                {
                    //Console.WriteLine(ioitem.FullName);
                    SubFoldersLookup(ioitem.FullName);

                }
            }
        }

        //============================
        // Get Metadata
        //============================
        private IMetadataReader metadata;
        private List<IMetadataReader> _metaList;

        /// <summary>
        /// runs a foreach, and calls GetFileMetaData for each element in the list musicList
        /// </summary>
        /// <param name="musikList">A list of musiknumbers, of type IOitems</param>
        private void GetAllMetaData(IEnumerable<IOitem> musikList)
        {
            if (musikList == null) throw new ArgumentNullException("musikList");

            foreach (IOitem musicnumber in musikList)
            {
                GetFileMetaData(musicnumber.FilePath, musicnumber.Name);
            }
        }

        /// <summary>
        /// Calls the Metadata.MetadataReader with the passed parameters, and places the returned data to 
        /// the list _metaList
        /// </summary>
        /// <param name="folderPath">A path to the musicnumber</param>
        /// <param name="musicnumber">The musicnumber that are to be pased on to the metadataReader</param>
        private void GetFileMetaData(string folderPath, string musicnumber)
        {
            metadata = new Metadata.MetadataReader(folderPath, musicnumber);
            _metaList.Add(metadata);
        }

        //============================
        // This function returns the metadata.
        //============================

        /// <summary>
        /// A public function to return the private list _metaList.
        /// </summary>
        /// <returns>returns a list of object with metadata</returns>
        public List<IMetadataReader> GetMetaData()
        {
            return _metaList;
        }
        
    }
}
