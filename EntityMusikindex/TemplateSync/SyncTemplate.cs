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
using Live555;


namespace TemplateSync
{
    class SyncTemplate
    {
        private string _AbsPath;
        private string _RelPath;
        private Dbhandel db = new Dbhandel();
        private Live555Wrapper live555;
        //private int _index;


       public SyncTemplate()
       {
           
           live555 = new Live555Wrapper();
           string ip = live555.GetIP();

           db.FillIP(ip); // denne skal hentes fra live555
          
         
        }

        public void Sync(List<string> pathlist )
        {
            db.FillPath(pathlist);
            // live555.addpathes(pathlist)

            foreach (var path in pathlist)
            {
                var indexer = new FileIndexer(path);

                List<IMetadataReader> mdata = indexer.GetMetaData();

                db.Album_Artist_Genre_Adders(mdata);

                db.fillMusicdata(mdata);
            }


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
