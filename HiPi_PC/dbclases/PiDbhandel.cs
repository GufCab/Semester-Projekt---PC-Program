using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace dbclases
{
    /// <summary>
    /// this is the interface used to manipulate the PIdb.
    /// </summary>
    public interface IPidbhandel
    {
        void Markasonline();

        void SyncfromLocalToPI();

        void Markasofline();
    }

    /// <summary>
    /// This class takes the tables from the Local and adds their content to the Pi database.
    /// </summary>
   public class PiDbhandel : IPidbhandel
    {
       /// <summary>
       /// this function marks a device as online, or makes a new device and marks it as online.
       /// </summary>
        public void Markasonline()
       {
           {
               if (!CheckIfDeviceExists())
               {
                   List<device> pcdevices;
                   using (var musik = new pcindexEntities())
                   
                   {
                       
                       pcdevices = (from p in musik.devices select p).ToList();
                   }

                   var nyed = pcdevices.ElementAt(0);
                   var pid = new PIDevice();
                   pid.UUIDDevice = nyed.UUIDDevice;
                   pid.IP = nyed.IP;
                   pid.PCOwner = nyed.PCOwner;
                   pid.Protocol = nyed.Protocol;
                   pid.Catagory_idCatagory = 1;
                   pid.Online = true;

                   using (var pimusik = new PiindexEntities())
                   {
                       pimusik.PIDevices.Add(pid);
                       pimusik.SaveChanges();
                   }

               }
               else
               {
                   FixIpSetAsOnline();
               }
           }

       }

       /// <summary>
       /// this funcktion fixes the ip the Pi database, and marks this device as online.
       /// </summary>
       private void FixIpSetAsOnline()
       {
           List<device> PCdevices;
           using (var musik = new pcindexEntities())
           {
               PCdevices = (from p in musik.devices select p).ToList();
           }

           var my = PCdevices[0];
           using (var pimusik = new PiindexEntities())
           {
               var change = (from p in pimusik.PIDevices where p.UUIDDevice == my.UUIDDevice select p).ToList();
               change[0].IP = my.IP;
               change[0].Online = true;
               pimusik.SaveChanges();
           }

       }
       /// <summary>
       /// this function synchronizes the Pidatabase with Pc database
       /// </summary>
       public void SyncfromLocalToPI()
        {
            FillPiPath();
            FillAAG();
            FillMusikData();

        }
       /// <summary>
       /// this function marks a device as ofline in the Pi database
       /// </summary>
        public void Markasofline()
        {
            throw new NotImplementedException();
        }

       /// <summary>
       /// returns true if the device is in the pi database
       /// </summary>
       /// <returns></returns>
        private bool CheckIfDeviceExists()
        {
            List<string> pcdevices;
            using (var musik = new pcindexEntities())
            {
              pcdevices = (from p in musik.devices select p.UUIDDevice).ToList();
            }
            using (var pimusik = new PiindexEntities())
            {
                pcdevices = ListCompare(pcdevices, (from p in pimusik.PIDevices select p.UUIDDevice).ToList());
            }
            return pcdevices.Count == 0;
        }

       /// <summary>
       /// Takes the Pathes from the pc database and adds the new to the Pi database. 
       /// </summary>
       private void FillPiPath()
       {
           List<filepath> pathtopi;
           List<PIFilePath> pifilepaths;

           using (var musik = new pcindexEntities())
           {
               pathtopi = (from p in musik.filepaths select p).ToList();
           }

           using (var pimusik = new PiindexEntities())
           {
               pifilepaths = (from p in pimusik.PIFilePaths select p).ToList();
           }

           pathtopi = ListCompare(pathtopi, pifilepaths);

           foreach (var ele in pathtopi)
               {
                   var fil = new PIFilePath();
                   fil.Device_UUIDDevice = ele.Device_UUIDDevice;
                   fil.FilePath = ele.FilePath1;
                   fil.UUIDPath = ele.UUIDPath;
                   using (var pimusik = new PiindexEntities())
                   {
                       pimusik.PIFilePaths.Add(fil);
                       pimusik.SaveChanges();
                   }
                  
               }
           
       }
       /// <summary>
       /// Compares the pathlist on Pc to pathlist
       /// </summary>
       /// <param name="pcList"></param>
       /// <param name="pilist"></param>
       /// <returns></returns>
       private List<filepath> ListCompare(List<filepath> pcList, List<PIFilePath> pilist)
       {
           List<filepath> afa = new List<filepath>();


           foreach (var pcPath in pcList)
           {
               foreach (var pipath in pilist)
               {
                   if (pcPath.UUIDPath == pipath.UUIDPath)
                       afa.Add(pcPath);
               }
           }

           foreach (var faf in afa)
           {
               pcList.Remove(faf);
           }

           return pcList;
       }
       /// <summary>
       /// Compares 2 lists of strings  
       /// </summary>
       /// <param name="list1">the list to change</param>
       /// <param name="list2">List from piDatabase</param>
       /// <returns>list1</returns>
       private List<string> ListCompare(List<string> list1, List<string> list2)
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
       /// <summary>
       /// Fills Artist,Album, Genre from local to pi Database
       /// </summary>
       private void FillAAG()
       {
           List<string> Artist;
           List<string> Album;
           List<string> Genre;

           using (var musik = new pcindexEntities())
           {
                Artist = (from p in musik.artists select p.Artist1).ToList();
                Album = (from p in musik.albums select p.Album1).ToList();
                Genre = (from p in musik.genres select p.Genre1).ToList();
           }

           using (var pimusik = new PiindexEntities())
           {
               Artist = ListCompare(Artist,(from p in pimusik.PIArtists select p.Artist).ToList());
               Album = ListCompare(Album  ,(from p in pimusik.PIAlbums select p.Album).ToList());
               Genre = ListCompare(Genre  ,(from p in pimusik.PIGenres select p.Genre).ToList());
           }

           if (Artist.Count > 0)
               addArtist(Artist);
           if (Album.Count > 0)
               addAlbum(Album);
           if (Genre.Count > 0)
               addGenre(Genre);

       }
       /// <summary>
       /// Adds Artist to PiDatabase
       /// </summary>
       /// <param name="aList">List of Artist</param>
       private void addArtist(List<string> aList)
       {
           foreach (var onpath in aList)
           {
               var _item = new PIArtist();
               _item.Artist = onpath;
               _item.Musik_Catagory_idCatagory = 1;

               using (var pimusik = new PiindexEntities())
               {
                   pimusik.PIArtists.Add(_item);
                   pimusik.SaveChanges();
               }
           }
       }
       /// <summary>
       /// Adds Albumlist to Database.
       /// </summary>
       /// <param name="aList">list of Albums</param>
       private void addAlbum(List<string> aList)
       {
           foreach (var onpath in aList)
           {
               var _item = new PIAlbum();
               _item.Album = onpath;
               _item.Musik_Catagory_idCatagory = 2;

               using (var pimusik = new PiindexEntities())
               {
                   pimusik.PIAlbums.Add(_item);
                   pimusik.SaveChanges();
               }
           }
       }
       /// <summary>
       /// Adds Genrelist to Database.
       /// </summary>
       /// <param name="aList">list of Genres</param>
       private void addGenre(List<string> aList )
       {
           foreach (var onpath in aList)
           {
               var _item = new PIGenre();
               _item.Genre = onpath;
               _item.Musik_Catagory_idCatagory = 3;

               using (var pimusik = new PiindexEntities())
               {
                   pimusik.PIGenres.Add(_item);
                   pimusik.SaveChanges();
               }
           }
       }
       // Takes all music from Pc an adds to the Pi what it doesn't have

       /// <summary>
       /// Fills Music data from LocalDB to PIDB.
       /// </summary>
       private void FillMusikData()
       {
           List<musicdata> musicdatas;

           using (var musik = new pcindexEntities())
           {
               musicdatas = (from p in musik.musicdatas select p).ToList();
           }
           using (var pimusik = new PiindexEntities())
           {
               musicdatas = ListCompare(musicdatas, (from p in pimusik.PIMusikDatas select p).ToList());               
           }

           if(musicdatas.Count >0)
           Addmusikdata(musicdatas);

       }
       /// <summary>
       /// Compares 2 lists of Metadata  
       /// </summary>
       /// <param name="list1">the list to change</param>
       /// <param name="list2">List from piDatabase</param>
       /// <returns>list1</returns>
       private List<musicdata> ListCompare(List<musicdata> pcList, List<PIMusikData> pilist)
       {
           List<musicdata> afa = new List<musicdata>();


           foreach (var pcPath in pcList)
           {
               foreach (var pipath in pilist)
               {
                   if (pcPath.UUIDMusikData == pipath.UUIDMusikData)
                       afa.Add(pcPath);
               }
           }
           foreach (var faf in afa)
           {
               pcList.Remove(faf);
           }
           return pcList;
       }

       /// <summary>
       /// Adds A list of musicdata to PIDB
       /// </summary>
       /// <param name="aList">list of musicdata</param>
       private void Addmusikdata(List<musicdata> aList)
       {
           foreach (var onpath in aList)
           {
               var _item = new PIMusikData();
               _item.UUIDMusikData = onpath.UUIDMusikData;
               _item.Title = onpath.Title;
               _item.NrLenth = onpath.NrLenth;
               _item.FileName = onpath.FileName;
               _item.Artist_Artist = onpath.Artist_Artist;
               _item.Album_Album = onpath.Album_Album;
               _item.Genre_Genre = onpath.Genre_Genre;
               _item.FilePath_UUIDPath = onpath.FilePath_UUIDPath;

               using (var pimusik = new PiindexEntities())
               {
                   pimusik.PIMusikDatas.Add(_item);
                   pimusik.SaveChanges();
               }
           }
       }
    }
}
