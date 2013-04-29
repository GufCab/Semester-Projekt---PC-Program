using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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


            Uri to = new Uri(@""+a);
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

           // List<IMetadata> metalist;

            using (var musik = new musikindexEntities())
            {

                //metalist = getmetadata();
                var nummer = new musikdata();
                //foreach (var metadata in metalist)
                //{
                //    nummer.Titel = metadata.Title;
                //    nummer.Kunstner = metadata.Kunstner;
                //    nummer.Album = metadata.Album;
                //    nummer.Genre = metadata.Genre;
                //    nummer.nrLenth = metadata.Nrlenth;
                //    //nummer.FilepathTabel_idFilesti = metadata.FilePath;

                //}

                
                //foreach (var metadata in _datalist)
                {
                    //nummer.Titel = metadata.Title;
                    //nummer.Kunstner = metadata.Kunstner;
                    //nummer.Album = metadata.Album;
                    //nummer.Genre = metadata.Genre;
                    //nummer.nrLenth = metadata.Nrlenth;

                    nummer.Titel = "jump";
                    nummer.Kunstner = "Van Halen";
                    nummer.Album = "Blackbox";
                    nummer.Genre = "babyrock";
                    nummer.nrLenth = 334;

                    nummer.Filesti_idFilesti = 2;//ref til den rigtige absPath





                    musik.musikdatas.Add(nummer);
                }

               

                musik.SaveChanges();

            }

        }

        public void FillIP()
        {
            using (var musik = new musikindexEntities())
            {
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);

                var myIP = new ip();

                myIP.idIP = ip.AddressList[0].ToString();

                myIP.ejer = Environment.UserName;

                musik.ips.Add(myIP);

                musik.SaveChanges();
            }

        }

        public void FillPath()
        {

            var sti = new filesti();
            sti.Filesti1 = _RelPath;
            //_index = sti.idFilesti;

            using (var musik = new musikindexEntities())
            {  
                musik.filestis.Add(sti);
                               
                musik.SaveChanges();
                
            }

        }

        public void CreateStream()
        {
            //setup af live555 stream

        }

       public void MetadataGetter()
       {
           


       }
    }
}
