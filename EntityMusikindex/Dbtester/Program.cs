using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaReader.FileIndexer;
using MetaReader.MetadataReader;

namespace Dbtester
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbh = new dbclases.Dbhandel();

            //dbh.FillIP("192.168.001.190");

            //List<string> ha = new List<string>();

            //ha.Add("dancfdfe1");
            //ha.Add("hafdfdps1");

            //dbh.FillPath(ha);

            //List<string> haps = new List<string>();

            //haps.Add("js4j4s");

            //dbh.Addgenre(haps);

            //haps.Add("js4j4fdsfs");

            //dbh.addArtist(haps);
            //dbh.addAlbum(haps);

            var index = new FileIndexer(@"C:\Users\Public\Music\Sample Music");

            List<IMetadataReader> mdata = index.GetMetaData();

            dbh.Album_Artist_Genre_Adders(mdata);

            dbh.fillMusicdata(mdata);
        }
    }
}
