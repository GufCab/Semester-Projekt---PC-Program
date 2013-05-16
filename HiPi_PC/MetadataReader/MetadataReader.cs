using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shell32;

namespace MetaReader.MetadataReader
{
    public class MetadataReader : IMetadataReader
    {
        //other variables
        private int _objectnumber;
        private string _folder;


        //Return types
        public string ItemName { get; private set; }
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public string Album { get; private set; }
        public string Nr { get; private set; }
        public string Genre { get; private set; }
        public int LengthS { get; private set; }
        public string Filepath { get; private set; }

        //shell32 magic
        private readonly Shell32.Shell _shell = new Shell32.Shell();
        private Shell32.Folder _objFolder;
        private Shell32.FolderItem2 _item2;

        public MetadataReader(int objectnumber, string folder)
        {
            _objectnumber = objectnumber;
            _folder = folder;
            Setup();
            Setter();
        }

        private void Setup()
        {
            //select folder
            _objFolder = _shell.NameSpace(_folder);
            //select item
            int i = 0;
            foreach (Shell32.FolderItem2 folderItem2 in _objFolder.Items())
            {
                if (i == _objectnumber)
                    _item2 = folderItem2;
                i++;
            }
            
        }
        
        private void Setter()
        {
            ItemName = ArrHeader(0);
            Title = ArrHeader(21);
            Album = ArrHeader(14);
            Artist = ArrHeader(20);
            Nr = ArrHeader(26);
            Genre = ArrHeader(16);
            //LengthS = Convert.ToInt16(ArrHeader(27));
            Filepath = _folder;
        }

        private string ArrHeader(int Column)
        {
            return _objFolder.GetDetailsOf(_item2, Column);
        }
    }
}
