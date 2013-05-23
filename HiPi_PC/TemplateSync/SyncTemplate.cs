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
    public class SyncTemplate : ISynchronizer
    {
        private LocalDbhandel db = new LocalDbhandel();
        // Pidatabasen skal også være her
        private Live555Wrapper live555;
        //private int _index;

        private string _ip;

        public SyncTemplate()
        {
            Startup();
        }
      
       public override void Startup()
       {
           live555 = new Live555Wrapper();
           _ip = live555.GetIP();
           

       }
      
        
       public override void SyncPiDb()
       {
           var pidb = new PiDbhandel();

           pidb.Markasonline();
           pidb.SyncfromLocalToPI();
       }

        public override async void SyncLocalDb(List<string> pathlist )
        {

            db.FillIP(_ip);
            List<string> rellist = new List<string>();
            foreach (string s in pathlist)
            {
                rellist.Add(MakeRelpathFromAbspath(s));                
            }
            db.Fillrest(rellist);

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
                );
            relativePath = "../" + relativePath;
            relativePath = relativePath.Replace('/', Path.DirectorySeparatorChar);
            return  relativePath;
        }
        
    }
}
