using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaReader.FileIndexer;
using MetaReader.MetadataReader;

namespace LocalDbTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbh = new dbclases.LocalDbhandel();

            dbh.FillIP("12.2.12.12");

            var ha = new List<string>();
            ha.Add(@"..\..\..\..\..\..\..\Public\Music\Sample Music");

            dbh.FillPath(ha);

            var index = new FileIndexer(@"C:\Users\Public\Music\Sample Music");

            List<IMetadataReader> mdata = index.GetMetaData();

            dbh.Album_Artist_Genre_Adders(mdata);
            dbh.fillMusicdata(mdata);
        }
    }
}
