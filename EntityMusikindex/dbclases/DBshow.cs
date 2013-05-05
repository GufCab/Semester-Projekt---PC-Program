using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbclases
{
    class DBshow
    {


        public void showallmusic()
    {
            using (var musik = new musikindexEntities())
            {

                var allmusic =  (from p in musik.musikdatas select new {Title = p.Title, Artist = p.Artist_idArtist, Album = p.Album_idAlbum, Genre = p.Genre_Genre}).ToList();

            }


    }




    }
}
