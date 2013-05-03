using System;
using System.Collections.Generic;
using Shell32;

namespace MetaReader.MDataReader
{
    class Reader
    {
        //readonly List<MetaData> _MetaData = new List<MetaData>(); 
        readonly List<string> _arrHeaders = new List<string>();

        readonly Shell32.Shell _shell = new Shell32.Shell();
        Shell32.Folder _objFolder;

  //      private string _foldername = "C:\Users\Public\Music\Sample Music";
        public String Foldername { get; private set; }

        public Reader()
        {
            Foldername = @"C:\Users\Jakob\Music\";
            SelectFolder();
        }

        public Reader(string foldername)
        {
            //Foldername;
            Foldername = foldername;
            SelectFolder();        
        }

        public void SelectFolder()
        {
            _objFolder = _shell.NameSpace(Foldername);
        }

        public void PrintArrHeader()
        {
            foreach (var arrHeader in _arrHeaders)
            {
                Console.WriteLine(arrHeader);
            }
            
        }

        public string GetTitle(Shell32.FolderItem2 item2)
        {
            string title = _objFolder.GetDetailsOf(item2, 0);
            return title;
        }

        public string GetItem(Shell32.FolderItem2 Item2)
        {

            return "Not implemented";
        }

        public void AccessData()
        {

            for (int i = 0; i < short.MaxValue; i++)
            {
                string header = _objFolder.GetDetailsOf(null, i);
                if (String.IsNullOrEmpty(header))
                    break;
                _arrHeaders.Add(header);
            }
        }

        public string PrintTitle()
        {

            if (_arrHeaders[0] == "Title")
            {
                Console.WriteLine("True");
            }

            //Shell32.FolderItem2 in 

            //string temp = Shell32.FolderItem2 in _objFolder.Items();

            string _title = _objFolder.GetDetailsOf(_objFolder.Items(),0);
            //string _
            foreach (Shell32.FolderItem2 item in _objFolder.Items())
            {
                Console.WriteLine("{0}", _objFolder.GetDetailsOf(item, 0));
            }

            //Shell32.FolderItem2 item in _objFolder.Items();

            Console.WriteLine("{0}: {1}",0, _title);

            return _title;
        }

        public void PrintItem()
        {
            foreach (Shell32.FolderItem2 item in _objFolder.Items())
            {
                Console.WriteLine(GetTitle(item));

            }
        }



        public void PrintOne(int index)
        {
            int size = _objFolder.Items().Count;
            if(index >= size)
                return;

            var tempItem2 = new FolderItem2[size];
            //Shell32.FolderItem2 tempItem = _objFolder.ParseName()


            //tempItem2 = _objFolder.Items();

            int j = 0;
            foreach (Shell32.FolderItem2 tempItem in _objFolder.Items())
            {
                tempItem2[j] = tempItem;
                j++;
            }

            //foreach (Shell32.FolderItem2 item in _objFolder.Items())
            {
                for (int i = 0; i < _arrHeaders.Count; i++)
                {
                    Console.WriteLine("{0}\t{1}: {2}", index, _arrHeaders[i], _objFolder.GetDetailsOf(tempItem2[index],i));
                }
            }
        }

        public void Print()
        {

            foreach (Shell32.FolderItem2 item in _objFolder.Items())
            {
                //for (int i = 0; i < arrHeaders.Count; i++)
                for (int i = 0; i < 10; i++) //this prints the metadata <--
                {
                    Console.WriteLine("{0}\t{1}: {2}", i, _arrHeaders[i], _objFolder.GetDetailsOf(item, i));
                }

            }
        }
    }
}
