using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiDbTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbh = new dbclases.PiDbhandel();



            dbh.Markasonline();
            dbh.SyncfromLocalToPI();

        }
    }
}
