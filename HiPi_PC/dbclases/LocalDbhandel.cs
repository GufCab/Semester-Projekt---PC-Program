using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using MetadataReader.FileIndexer;
using MetadataReader.Metadata;


namespace dbclases
{
    public interface ILocalDbhandel
    {
        void FillIp(string ip);

        void FillMusicAndPath(List<string> Pathlist);
    }

    public class LocalDbhandel : ILocalDbhandel
    {
        private string _GUIDDevice;
        
        private bool makeguuidifnodevice()
        {
            using (var musik = new pcindexEntities())
            {
                var GUIDDevice = (from p in musik.devices select p).ToList();

                if (GUIDDevice.Count == 0)
                    return false;

                _GUIDDevice = GUIDDevice[0].UUIDDevice;
                return true;

            }
        }

        public void FillIp(string myip)
        {            
                if (!makeguuidifnodevice())
                {
                    var mydevice = new device();

                    mydevice.UUIDDevice = Guid.NewGuid().ToString();
                    _GUIDDevice = mydevice.UUIDDevice;
                    mydevice.IP = myip+":8554";
                    mydevice.Protocol = "rtsp://";
                    mydevice.PCOwner = Environment.UserName;

                    using (var musik = new pcindexEntities())
                    {
                        musik.devices.Add(mydevice);
                        musik.SaveChanges();
                    }
                   
                }
                else
                {
                    using (var musik = new pcindexEntities())
                    {
                        var olddevice = (from p in musik.devices select p).ToList();
                        if (myip != olddevice[0].IP)
                        {
                            olddevice[0].IP = myip+":8554";

                            musik.SaveChanges();

                        }                      
                    }
                  
                }
            
        }

        private List<string> Albumlist = new List<string>();
        private List<string> Artistlist = new List<string>();
        private List<string> Genrelist = new List<string>(); 

        public void FillAAG(List<IMetadataReader> metadataReaders)
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

        public void FillMusicAndPath(List<string> PathOndevice)
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

                        IFileIndexer indexer = new FolderAndFileReader();
                        indexer.SetIndexPath(onpath);

                        List<IMetadataReader> mdata = indexer.GetMetaData();

                        FillAAG(mdata);
                        fillMusicdata(mdata,path.UUIDPath);



                    }


                }

            }
            //see if pathes is allready is assigned to ip
            // if not add pathes

        }


        private List<string> listcompair(List<string> list1, List<string> list2)
        {
            List<string> toremove = new List<string>();

            list1 = list1.Distinct().ToList();


            foreach (var s1 in list1)
            {
                foreach (var s2 in list2)
                {
                    if (s1 == s2)
                        toremove.Add(s1);
                }
            }

            foreach (var faf in toremove)
            {
                list1.Remove(faf);
            }
            toremove.Clear();

            return list1;
        }
        

        public void fillMusicdata(List<IMetadataReader> datalist,string uuid)
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
                    nummer.FilePath_UUIDPath = uuid;
                    nummer.UUIDMusikData = Guid.NewGuid().ToString();

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
            // remove duplets
          


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
