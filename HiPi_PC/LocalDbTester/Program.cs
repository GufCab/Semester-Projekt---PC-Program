using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDbTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbh = new dbclases.LocalDbhandel();

            dbh.selectallgenres();

            dbh.FillIP("192.168.1.90");

            var ha = new List<string>();
            ha.Add(@"C:\Users\Public\Music");

            dbh.FillPath(ha);


        }
    }
}
