using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MetaReader.FileIndexer;
using MetaReader.MetadataReader;


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
           //Creating Path's
            _AbsPath = absPath;

           _RelPath = MakeRelpathFromAbspath(absPath);

           FillIP(); 

           FillPath();
          
           FillDatabase();

            CreateStream();// RelPath is path from Live555 to Musicfolder
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

        public void FillDatabase()
        {
            var index = new FileIndexer(_AbsPath);

            List<IMetadataReader> mdata = index.GetMetaData();

            var nummer = new musikdata();
           

            using (var musik = new musikindexEntities1())
            {


                foreach (var metadata in mdata)
                {
                    nummer.Title = metadata.Title;
                    nummer.Artist = metadata.Artist;
                    nummer.Album = metadata.Album;
                    nummer.Genre = metadata.Genre;
                    nummer.NrLenth = metadata.LengthS;
                    nummer.FileName = metadata.ItemName;
                    nummer.FilePath_idFilePath = _RelPath;

                    musik.musikdatas.Add(nummer);

                    musik.SaveChanges();
                }
                

            

            }

        }

        public void FillIP()
        {
            using (var musik = new musikindexEntities1())
            {
                

                string host = Dns.GetHostName();
                IPHostEntry IPHost = Dns.GetHostEntry(Dns.GetHostName());

                var myIP = new ip();

                //myIP.idIP = IPHost.AddressList[0].ToString();
                myIP.idIP = "192.168.001.090";

                var ip = (from p in musik.ips select p.idIP);

                bool chek =false;

                foreach (var i in ip)
                {
                    chek = i.ToString() == myIP.idIP ? true : false;                    
                }
 

                if(!chek)
                {
                    myIP.Owner = Environment.UserName;

                    myIP.Protocol = "rtsp://";

                    musik.ips.Add(myIP);

                    musik.SaveChanges();
                }
               
            }

        }

        public void FillPath()
        {

            var sti = new filepath();

            sti.IP_idIP = "192.168.001.090";//egen ip getter skal laves som funktion
            sti.idFilePath = _RelPath;
            //_index = sti.idFilesti;

            using (var musik = new musikindexEntities1())
            {  
                musik.filepaths.Add(sti);
                               
                musik.SaveChanges();
                
            }

        }

        public void CreateStream()
        {
            //setup af live555 stream

        }


    }
}
