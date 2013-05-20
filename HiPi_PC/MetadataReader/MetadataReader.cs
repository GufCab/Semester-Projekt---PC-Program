using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shell32;

namespace MetaReader.MetadataReader
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

        public MetadataReader(string folderpath, string musikNumber)
        {
            _musikNumber = musikNumber;
            _folder = folderpath;
            _objFolder = _shell.NameSpace(_folder);
            findItem();
            Setter();
        }

        private void findItem()
        {
            foreach (Shell32.FolderItem2 folderItem2 in _objFolder.Items())
            {
                string tempItemName = folderItem2.Name + Path.GetExtension(_objFolder.GetDetailsOf(folderItem2, 180)); //_objFolder.GetDetailsOf(folderItem2, 180); 180: file with path and extension
                if (tempItemName == _musikNumber)
                    _item2 = folderItem2;
            }

        }

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

        private string SetItemName()
        {
            return _item2.Name + Path.GetExtension(_objFolder.GetDetailsOf(_item2, 180));
        }

        private string flipBackslashes(string name)
        {
            var _name = name.Replace('\\', '/');
            return _name;
        }

        private int ConvertLength(string lenght)
        {
            double tempTime = TimeSpan.Parse(lenght).TotalSeconds;

            return Convert.ToInt32(tempTime);
        }

        private string ArrHeader(int Column)
        {
            return _objFolder.GetDetailsOf(_item2, Column);
            //return _item2
        }
    }
}
