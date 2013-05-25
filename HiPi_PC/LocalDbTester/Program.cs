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

            dbh.FillIp("192.168.1.11");

            var ha = new List<string>();
            ha.Add(@"C:\Users\Malmos\Documents\GitHub\Semester-Projekt---PC-Program\musikmappe");

            dbh.FillMusicAndPath(ha);

            
            
        }
    }
}
