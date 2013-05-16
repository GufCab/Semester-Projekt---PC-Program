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
        private Dbhandel db = new Dbhandel();
        private Live555Wrapper live555;
        //private int _index;


      
       public void Startup()
       {
           //live555 = new Live555Wrapper();
           //string ip = live555.GetIP();

           db.FillIP("100.199.100.199");

           // denne skal hentes fra live555
          
       }

       public void SyncLocalDb(List<string> pahts)
       {
           throw new NotImplementedException();
       }

       public void SyncPiDb()
       {
           throw new NotImplementedException();
       }

        public async void Sync(List<string> pathlist )
        {
            List<string> rellist = new List<string>();
            foreach (string s in pathlist)
            {
                rellist.Add(MakeRelpathFromAbspath(s));                
            }

            var slowtast = Task.Factory.StartNew(() => db.FillPath(rellist));
            
            await slowtast;
            slowtast = Task.Factory.StartNew(() => hs(rellist));

            await slowtast;
            

            // live555.addpathes(pathlist)

            


        }
        public void hs(List<string> ha)
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


        public void CreateStream()
        {
            //setup af live555 stream

        }


        
    }
}
