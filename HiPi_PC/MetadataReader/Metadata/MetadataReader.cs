using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shell32;

namespace MetadataReader.Metadata
{
    class MetadataReader : IMetadataReader
    {
        //other variables
        //private int _objectnumber;
        private string _folder;


        //Return types
        public string ItemName { get; private set; }
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public string Album { get; private set; }
        public string Nr { get; private set; }
        public string Genre { get; private set; }
        public int LengthS { get; private set; }
        public string Lengthstring { get; private set; } 
        public string Filepath { get; private set; }

        //shell32 magic
        private readonly Shell32.Shell _shell = new Shell32.Shell();

        private Shell32.Folder _objFolder;
        private Shell32.FolderItem2 _item2;

        private string _musikNumber;

        /// <summary>
        /// Constuctor that sets the private variables, and calls the findItem and Setter function
        /// </summary>
        /// <param name="folderpath">The path with the musicnumber</param>
        /// <param name="musicNumber">The musicnumber the metadataReader needs to find metadata about</param>
        public MetadataReader(string folderpath, string musicNumber)
        {
            _musikNumber = musicNumber;
            _folder = folderpath;
            _objFolder = _shell.NameSpace(_folder);
            findItem();
            Setter();
        }

        /// <summary>
        /// Finds the Shell32.FolderItem2, that is similiar to the musicNumber, that is given in the constructor
        /// </summary>
        private void findItem()
        {
            foreach (Shell32.FolderItem2 folderItem2 in _objFolder.Items())
            {
                string tempItemName = folderItem2.Name;
                    // + Path.GetExtension(_objFolder.GetDetailsOf(folderItem2, 180)); //_objFolder.GetDetailsOf(folderItem2, 180); 180: file with path and extension
                if (tempItemName == _musikNumber)
                {
                    _item2 = folderItem2;
                    break;
                }
            }

        }

        /// <summary>
        /// Sets the puplic variables that exists in the interface
        /// </summary>
        private void Setter()
        {
            //ItemName = ArrHeader(0);
            ItemName = SetItemName();
            Title = ArrHeader(21);
            Album = ArrHeader(14);
            Artist = ArrHeader(20);
            Nr = ArrHeader(26);
            Genre = ArrHeader(16);
            LengthS = ConvertLength(ArrHeader(27));
            Lengthstring = ArrHeader(27);
            Filepath = flipBackslashes(_folder);
        }

        /// <summary>
        /// Sets the the items name, with file extention.
        /// </summary>
        /// <returns>returns the filename with extentions</returns>
        private string SetItemName()
        {
            //item2.name creates a null reference
            if (_item2 == null)
            {
                return "ERROR in SetItemName";
            }
            return _item2.Name;// +Path.GetExtension(_objFolder.GetDetailsOf(_item2, 180));
        }

        /// <summary>
        /// Becourse of linux, this ensures that all slashes, are forwardslashes
        /// </summary>
        /// <param name="name">A string that has backslashes that needs flipping</param>
        /// <returns>The string with forwardslashes</returns>
        private string flipBackslashes(string name)
        {
            var _name = name.Replace('\\', '/');
            return _name;
        }

        /// <summary>
        /// Changes the time to int from string
        /// </summary>
        /// <param name="lenght">A length in a string of format hh:mm:ss</param>
        /// <returns>the size in an int</returns>
        private int ConvertLength(string lenght)
        {
            if (lenght == null) throw new ArgumentNullException("lenght");

            double tempTime = TimeSpan.Parse(lenght).TotalSeconds;

            return Convert.ToInt32(tempTime);
        }

        /// <summary>
        /// Takes the collumn number of obj, and returns the value in the collumn as a string.
        /// Collumns contain the different metadata as songtitle, file type or many others 
        /// </summary>
        /// <param name="Column">The place of the needed metadata</param>
        /// <returns>The metadata from the requested metadata</returns>
        private string ArrHeader(int Column)
        {
            return _objFolder.GetDetailsOf(_item2, Column);
            //return _item2
        }
    }
}
