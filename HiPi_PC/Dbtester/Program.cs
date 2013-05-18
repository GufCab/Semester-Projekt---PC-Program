using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MetaReader.FileIndexer;
using MetaReader.MetadataReader;

namespace Dbtester
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbh = new dbclases.LocalDbhandel();

            dbh.FillIP("212.212.224.124");

            List<string>ha = new List<string>();
            ha.Add(@"C:\Users\Public\Music\Sample Music");

            dbh.FillPath(ha);

            var stop = new Stopwatch();

            stop.Start();
            var index = new FileIndexer(@"C:\Users\Public\Music\Sample Music");

            List<IMetadataReader> mdata = index.GetMetaData();

            Console.WriteLine(stop.ElapsedMilliseconds);


            dbh.Album_Artist_Genre_Adders(mdata);

            dbh.fillMusicdata(mdata);
            stop.Stop();
            Console.WriteLine(stop.ElapsedMilliseconds);

        }
    }
}
