using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MetaReader.FileIndexer;
using MetaReader.MetadataReader;
using dbclases;


namespace TemplateSync
{
    class SyncTemplate
    {
        //private List<IMetadata> _datalist;
        private string _AbsPath;
        private string _RelPath;
        //private int _index;


       public SyncTemplate(string absPath)
       {
           var db = new Dbhandel();
           //Creating Path's
            _AbsPath = absPath;

           _RelPath = MakeRelpathFromAbspath(absPath);

           CreateStream();// RelPath is path from Live555 to Musicfolder

           db.FillIP("192.168.001.190"); // denne skal hentes fra live555
          
         
        }

        public void Sync()
        {
            //denne skal synce alle data

        }

        private string MakeRelpathFromAbspath(string a)
        {


            Uri to = new Uri(a);
            // Must end in a slash to indicate folder
            Uri from = new Uri(Environment.CurrentDirectory);
            string relativePath =
            Uri.UnescapeDataString(
                from.MakeRelativeUri(to)
                    .ToString()
                    .Replace('/', Path.DirectorySeparatorChar)
                );


            return relativePath;
        }


        public void CreateStream()
        {
            //setup af live555 stream

        }


    }
}
