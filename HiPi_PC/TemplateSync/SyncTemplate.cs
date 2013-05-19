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
    class SyncTemplate : ISynchronizer
    {
        private LocalDbhandel db = new LocalDbhandel();
        // Pidatabasen skal også være her
        private Live555Wrapper live555;
        //private int _index;

        public SyncTemplate()
        {
            Startup();
        }
      
       public override void Startup()
       {
           live555 = new Live555Wrapper();
           string ip = live555.GetIP();

           db.FillIP(ip);
          
           // tell Pi device is online

       }
      
        
       public override void SyncPiDb()
       {
           var pidb = new PiDbhandel();

           pidb.Markasonline();
           pidb.SyncfromLocalToPI();
       }

        public override async void SyncLocalDb(List<string> pathlist )
        {
            List<string> rellist = new List<string>();
            foreach (string s in pathlist)
            {
                rellist.Add(MakeRelpathFromAbspath(s));                
            }
            db.FillPath(pathlist);
            if (pathlist.Count != 0)
                hs(pathlist);
            //her skal stå relpath begge steder men fileindexer kan ikke klare den
        }
 
        private void hs(List<string> ha)
        {

            foreach (var path in ha)
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
        
    }
}
