using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaReader.IOReader
{
    class DirectoryReader
    {
        
        private readonly List<string> _fileinfo;
        private readonly List<string> _folderlist;
        private readonly List<string> _extList;
        public List<string> AllFilesAndFolders { get; private set; }
        private readonly string _location;
        private readonly System.IO.DirectoryInfo _dir;


        public DirectoryReader(string location)
        {
            _location = location;
            _dir = new DirectoryInfo(location);
            _fileinfo = new List<string>();
            _folderlist = new List<string>();
            _extList = new List<string>();
            AllFilesAndFolders = new List<string>();
            LoadExtList(); //remember to load before usage
            FilesInFolder();
            FoldersInFolders();
            
        }

        private void LoadExtList() //remember to load before usage
        {
            AddExtension(".wma");
            AddExtension(".mp3");
        }

        public void AddExtension(string dotExtention)
        {
            if (_extList.IndexOf(dotExtention) == -1)
            {
                _extList.Add(dotExtention);
                Console.WriteLine("Loaded extension {0}", dotExtention);
            }

            else
            {
                Console.WriteLine("Extension already exist for {0}", dotExtention);
            }
        }

        //============================
        // Find all files in folder
        //============================
        private void FilesInFolder()
        {
            foreach (System.IO.FileInfo fileInfo in _dir.GetFiles("*.*"))
            {
                if (_extList.IndexOf(fileInfo.Extension) != -1)
                {
                    _fileinfo.Add(fileInfo.Name);
                    _fileinfo.Add(fileInfo.Extension);
                    AllFilesAndFolders.Add(fileInfo.Name);
                }
                else
                {
                    Console.WriteLine("Unhandled extention for {0}",fileInfo.Name);
                }
                //add metainfo : fileInfo.Length
                //add meteinfo : fileInfo.Extension
            }
        }
        /*
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location); 
        foreach (System.IO.FileInfo f in dir.GetFiles("*.*")) 
        { 
        //LOAD FILES 
        ListViewItem lSingleItem = listView1.Items.Add(f.Name); 
        //SUB ITEMS 
        lSingleItem.SubItems.Add(Convert.ToString(f.Length)); 
        lSingleItem.SubItems.Add(f.Extension); 
        }
        */
        //============================
        // Find all folders in folder
        //============================

        private void FoldersInFolders()
        {
            foreach (DirectoryInfo directoryInfo in _dir.GetDirectories())
            {
                string subFolder = directoryInfo.FullName;
                Console.WriteLine(subFolder);
                _folderlist.Add(directoryInfo.FullName);
                AllFilesAndFolders.Add(directoryInfo.Name);

                // This should be changed //
                Console.WriteLine("Enters subfolder {0}", subFolder);
                DirectoryReader subfolder = new DirectoryReader(subFolder);
                subfolder.PrintFilesAndFolders();

                foreach (string subfiles in subfolder.AllFilesAndFolders)
                {
                    this.AllFilesAndFolders.Add(subfiles);
                }
            }
        }

        private void OpenFolderAndLook()
        {

        }

        //============================
        // Printer
        //============================

        public void PrintFilesAndFolders()
        {
            foreach (string file in _fileinfo)
            {
                Console.WriteLine(file);
            }

            foreach (string folder in _folderlist)
            {
                Console.WriteLine(folder);
            }
        }

        /*
        TreeNode Main =  treeView1.Nodes.Add("Folders in: " + Location); 
        Main.Tag = ""; 
        foreach (System.IO.DirectoryInfo g in dir.GetDirectories()) 
        {     
        //LOAD FOLDERS 
        TreeNode MainNext = Main.Nodes.Add(g.FullName); 
        MainNext.Tag = (g.FullName); 
         * */
     } 
}

