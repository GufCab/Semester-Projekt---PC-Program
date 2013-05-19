﻿using System;
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
                var GUIDDevice = (from p in musik.devices select p).ToList();

                if (GUIDDevice.Count !=0)
                _GUIDDevice = GUIDDevice.ElementAt(0).UUIDDevice;
               
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
                var dd = (from p in musik.devices select p).ToList();

                if (dd.Count == 0)
                {
                    var mydevice = new device();

                    mydevice.UUIDDevice = Guid.NewGuid().ToString();
                    mydevice.IP = _ip;
                    mydevice.Protocol = "rtsp://";
                    mydevice.PCOwner = Environment.UserName;
                    musik.devices.Add(mydevice);
                    musik.SaveChanges();
                }
                else
                {
                    var mydevice = dd.ElementAt(0);
                    if (_ip != mydevice.IP)
                    {
                        musik.devices.Remove(dd.ElementAt(0));
                        musik.SaveChanges();
                        Console.WriteLine("de er ens");
                        mydevice.IP = _ip;
                        mydevice.Protocol = "rtsp://";
                        mydevice.PCOwner = Environment.UserName;
                        musik.devices.Add(mydevice);
                        musik.SaveChanges();
                    }
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

            addAlbum(Albumlist);
            addArtist(Artistlist);
            Addgenre(Genrelist);
                

        }

        public void FillPath(List<string> PathOndevice)
        {
            using (var musik = new pcindexEntities())
            {
                var pathlist = (from p in musik.filepaths
                                         select p.FilePath1

                                        ).ToList();



                PathOndevice = listcompair(PathOndevice, pathlist);



                if (PathOndevice.Count >= 1)
                {

                    foreach (var onpath in PathOndevice)
                    {
                        var path = new filepath();
                        path.Device_UUIDDevice = _GUIDDevice;
                        path.UUIDPath = Guid.NewGuid().ToString();
                        path.FilePath1 = onpath;
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

            using (var musik = new pcindexEntities())
            {


                foreach (var metadata in datalist)
                {
                    var nummer = new musicdata();
                    nummer.Title = metadata.Title;
                    nummer.Artist_Artist = metadata.Artist;
                    nummer.Album_Album = metadata.Album;
                    nummer.Genre_Genre = metadata.Genre;
                    nummer.NrLenth = metadata.LengthS;
                    nummer.FileName = metadata.ItemName;
                    nummer.FilePath_UUIDPath = UUIDONPath(metadata.Filepath);
                    nummer.UUIDMusikData = Guid.NewGuid().ToString();

                    musik.musicdatas.Add(nummer);

                    musik.SaveChanges();
                }


            }

        }
        
        private string UUIDONPath(string path)
        {

            using (var musik = new pcindexEntities())
            {
                var dd = (from p in musik.filepaths where p.FilePath1 == path select p).ToList();
                return dd.ElementAt(0).UUIDPath;
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