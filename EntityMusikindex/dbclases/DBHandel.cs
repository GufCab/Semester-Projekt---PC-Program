using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaReader.MetadataReader;


namespace dbclases
{
    public class Dbhandel
    {
        private string _ip;
        private List<string> _pathes; 

        public void FillIP(String myip)
        {
            _ip = @""+myip;
            // Check if id exist

            using (var musik = new musikindexEntities())
            {
                List<string> ips = (from p in musik.devices select p.idIP).ToList();

                bool check = false;
                foreach (var oldip in ips)
                {
                    if (oldip == myip)
                        check = true;

                }
                var mynewip = new device();
                mynewip.idIP = myip;
                mynewip.Owner = Environment.UserName;
                mynewip.Protocol = "rtsp://";
                mynewip.Catagory_idCatagory = 1;

                if (!check)
                {
                  
                    musik.devices.Add(mynewip);
                    musik.SaveChanges();

                }

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

        public void FillPath(List<string> PathOndevice)
        {
            using (var musik = new musikindexEntities())
            {
                List<string> pathlist = (from p in musik.filepaths
                                         //where p.IP_idIP == "192.168.001.090"
                                         where p.IP_idIP == _ip
                                         select p.idFilePath

                                        ).ToList();

                PathOndevice = listcompair(PathOndevice, pathlist);

                if (PathOndevice.Count >= 1)
                {
                    
                    foreach (var onpath in PathOndevice)
                    {
                        var path = new filepath();
                        path.idFilePath = onpath;
                        path.IP_idIP = _ip;

                        musik.filepaths.Add(path);
                        musik.SaveChanges();
                        
                    }
                   
                    
                }

            }
            //see if pathes is allready is assigned to ip
            // if not add pathes

        }

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
            var nummer = new musikdata();


            using (var musik = new musikindexEntities())
            {


                foreach (var metadata in datalist)
                {
                    nummer.Title = metadata.Title;
                    nummer.Artist_idArtist = metadata.Artist;
                    nummer.Album_idAlbum = metadata.Album;
                    nummer.Genre_Genre = metadata.Genre;
                    nummer.NrLenth = metadata.LengthS;
                    nummer.FileName = metadata.ItemName;
                    nummer.FilePath_idFilePath = "chomefuck";

                    musik.musikdatas.Add(nummer);

                    musik.SaveChanges();
                }


            }

        }

        public void Addgenre( List<string> liste)
        {
            //se if Genre exists if not add genre
            using (var musik = new musikindexEntities())
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
                        _genre.Musik_Catagory_idCatagory = 3;
                        

                        musik.genres.Add(_genre);
                        musik.SaveChanges();
                       
                    }

                    
                }
            }
        }

        public void addArtist(List<string> liste)
        {
            // make artist list

            using (var musik = new musikindexEntities())
            {
                List<string> list2 = (from p in musik.artists

                                      select p.idArtist

                                        ).ToList();

                liste = listcompair(liste, list2);



                if (liste.Count >= 1)
                {

                    foreach (var onpath in liste)
                    {
                        var entity = new artist();
                        entity.idArtist = onpath;
                        entity.Musik_Catagory_idCatagory = 1;


                        musik.artists.Add(entity);
                        musik.SaveChanges();

                    }


                }
            }
  



        }

        public void addAlbum(List<string> liste)
        {
            using (var musik = new musikindexEntities())
            {
                List<string> list2 = (from p in musik.albums

                                      select p.idAlbum

                                        ).ToList();

                liste = listcompair(liste, list2);



                if (liste.Count >= 1)
                {

                    foreach (var onpath in liste)
                    {
                        var entity = new album();
                        entity.idAlbum = onpath;
                        entity.Musik_Catagory_idCatagory = 2;


                        musik.albums.Add(entity);
                        musik.SaveChanges();

                    }


                }
            }

        }




    }
}
