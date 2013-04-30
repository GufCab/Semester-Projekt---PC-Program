using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaReader
{
    class MetaReader
    {
        readonly List<string> _arrHeaders = new List<string>();

        readonly Shell32.Shell _shell = new Shell32.Shell();
        Shell32.Folder _objFolder;

  //      private string _foldername = "C:\Users\Public\Music\Sample Music";
        private string _foldername;

        public void DataReaderSetup(string foldername)
        {
             _foldername = foldername;

             //_objFolder = _shell.NameSpace(@_foldername);
            _objFolder = _shell.NameSpace(@"C:\Users\Jakob\Music\");

            for (int i = 0; i < short.MaxValue; i++)
            {
                string header = _objFolder.GetDetailsOf(null, i);
                if (String.IsNullOrEmpty(header))
                    break;
                _arrHeaders.Add(header);
            }

            foreach (Shell32.FolderItem2 item in _objFolder.Items())
            {
                for (int i = 0; i < _arrHeaders.Count; i++)
                //for (int i = 0; i < 50; i++) //this prints the metadata <--
                {
                    Console.WriteLine("{0}\t{1}: {2}", i, _arrHeaders[i], _objFolder.GetDetailsOf(item, i));
                }

            }
        }
    }
}
