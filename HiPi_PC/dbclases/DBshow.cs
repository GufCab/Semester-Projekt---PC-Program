using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaReader.MetadataReader;
using MetadataReader;

namespace dbclases
{
    //public class DBshow : ObservableCollection<IMetadataReader>
    //{

    //    public void Showallmusic()
    //    {
    //        using (var musik = new musikindexEntities())
    //        {

    //           var hans =  (from p in musik.musikdatas select new {Title = p.Title, Artist = p.Artist_idArtist, Album = p.Album_idAlbum, Genre = p.Genre_Genre}).ToList();



    //            foreach (var elm in hans)
    //            {
    //                var sang = new Metadata();
    //                sang.Title = elm.Title;
    //                sang.Artist = elm.Artist;
    //                sang.Album = elm.Album;
    //                sang.Genre = elm.Genre;

    //                Add(sang);
    //            }
                
              
    //        }
    //    }
        
    //    public void ShowPlayqueue()
    //    {
    //        using (var musik = new musikindexEntities())
    //        {

    //            var playqueue = (from p in musik.playqueues select p).ToList();

    //        }
    //    }

        

    //}
}
