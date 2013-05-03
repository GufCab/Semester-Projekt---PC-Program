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

            var index = new FileIndexer(@"C:\Users\Public\Music\Sample Music");

            List<IMetadataReader> mdata = index.GetMetaData();

            dbh.Album_Artist_Genre_Adders(mdata);

            dbh.fillMusicdata(mdata);
        }
    }
}
