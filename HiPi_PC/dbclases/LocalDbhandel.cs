using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using MetaReader.MetadataReader;


namespace dbclases
{
    public class LocalDbhandel
    {
        private string _GUIDDevice;
        
        private void GetGUID()
        {
            using (var musik = new pcindexEntities())
            {
                List<string> GUIDDevice = (from p in musik.devices select p.UUIDDevise).ToList();

                _GUIDDevice = GUIDDevice.ElementAt(0);
            }
        }

        private string _ip;

        public void FillIP(String myip)
        {
            GetGUID();
            _ip = myip;
            // Check if id exist

            using (var musik = new pcindexEntities())
            {
                List<device> dd = (from p in musik.devices select p).ToList();

                device mydevice = dd.ElementAt(0);
                if (_GUIDDevice == mydevice.UUIDDevise)
                {
                    Console.WriteLine("de er ikke ens");
                }
                else
                {
                    Console.WriteLine("de er ikke ens");
                }
                

                //bool check = false;
                //foreach (var oldip in ips)
                //{
                //    if (oldip == myip)
                //        check = true;

                //}
                //var mynewip = new device();
                //mynewip.idIP = myip;
                //mynewip.Owner = Environment.UserName;
                //mynewip.Protocol = "rtsp://";
                //mynewip.Catagory_idCatagory = 1;

                //if (!check)
                //{

                //    musik.devices.Add(mynewip);
                //    musik.SaveChanges();

                //}

            }




        }

        private List<string> Albumlist = new List<string>();
        private List<string> Artistlist = new List<string>();
        private List<string> Genrelist = new List<string>(); 

        public void Album_Artist_Genre_Adders(List<IMetadataReader> metadataReaders)
        {
            foreach (var metadataReader in metadataReaders)
            {
              
                Albumlist.Add(metadataReader.Album);
                Artistlist.Add(metadataReader.Artist);
                Genrelist.Add(metadataReader.Genre);
            }

            Parallel.Invoke(
                () => addAlbum(Albumlist),
                () => addArtist(Artistlist),
                () => Addgenre(Genrelist)
                
                );

        }

        //public void FillPath(List<string> PathOndevice)
        //{
        //    using (var musik = new pcindexEntities())
        //    {
        //        List<string> pathlist = (from p in musik.filepaths
        //                                 //where p.IP_idIP == "192.168.001.090"
        //                                 where p.Device_UUIDDevise == _ip
        //                                 select p.idFilePath

        //                                ).ToList();



        //        PathOndevice = listcompair(PathOndevice, pathlist);

                

        //        if (PathOndevice.Count >= 1)
        //        {
                    
        //            foreach (var onpath in PathOndevice)
        //            {
        //                var path = new filepath();
        //                path.idFilePath = onpath;
        //                path.IP_idIP = _ip;

        //                musik.filepaths.Add(path);
        //                musik.SaveChanges();
                        
        //            }
                   
                    
        //        }

        //    }
        //    //see if pathes is allready is assigned to ip
        //    // if not add pathes

        //}

        private List<string> listcompair(List<string> list1, List<string> list2)
        {
            List<string> afa = new List<string>();


            foreach (var onPath in list1)
            {
                foreach (var dbpath in list2)
                {
                    if (onPath == dbpath)
                        afa.Add(onPath);
                }
            }

            foreach (var faf in afa)
            {
                list1.Remove(faf);
            }

            return list1;
        }
        

        public void fillMusicdata(List<IMetadataReader> datalist)
        {
            var nummer = new musicdata();


            using (var musik = new pcindexEntities())
            {


                foreach (var metadata in datalist)
                {
                    nummer.Title = metadata.Title;
                    nummer.Artist_Artist = metadata.Artist;
                    nummer.Album_Album = metadata.Album;
                    nummer.Genre_Genre = metadata.Genre;
                    nummer.NrLenth = metadata.LengthS;
                    nummer.FileName = metadata.ItemName;
                    //der mangler noget med noget filepaht
                    nummer.FilePath_UUIDPath = "112";

                    musik.musicdatas.Add(nummer);

                    musik.SaveChanges();
                }


            }

        }

        private void Addgenre( List<string> liste)
        {
            //se if Genre exists if not add genre
            using (var musik = new pcindexEntities())
            {
                List<string> list2 = (from p in musik.genres

                                         select p.Genre1

                                        ).ToList();

                liste = listcompair(liste, list2);

               

                if (liste.Count >= 1)
                {
                    
                    foreach (var onpath in liste)
                    {
                        var _genre = new genre();
                        _genre.Genre1 = onpath;
                        

                        musik.genres.Add(_genre);
                        musik.SaveChanges();
                       
                    }

                    
                }
            }
        }

        private void addArtist(List<string> liste)
        {
            // make artist list

            using (var musik = new pcindexEntities())
            {
                List<string> list2 = (from p in musik.artists

                                      select p.Artist1

                                        ).ToList();

                liste = listcompair(liste, list2);



                if (liste.Count >= 1)
                {

                    foreach (var onpath in liste)
                    {
                        var entity = new artist();
                        entity.Artist1 = onpath;


                        musik.artists.Add(entity);
                        musik.SaveChanges();

                    }


                }
            }
  



        }

        private void addAlbum(List<string> liste)
        {
            using (var musik = new pcindexEntities())
            {
                List<string> list2 = (from p in musik.albums

                                      select p.Album1

                                        ).ToList();

                liste = listcompair(liste, list2);



                if (liste.Count >= 1)
                {

                    foreach (var onpath in liste)
                    {
                        var entity = new album();
                        entity.Album1 = onpath;


                        musik.albums.Add(entity);
                        musik.SaveChanges();

                    }


                }
            }

        }

       
       
    }
}
