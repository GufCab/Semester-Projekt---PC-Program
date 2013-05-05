using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live555
{
    class Program
    {
        static void Main(string[] args)
        {
            var myLiveServer = new Live555Wrapper();
            Console.ReadLine();
            Console.WriteLine(myLiveServer.GetIP());
            Console.WriteLine(myLiveServer.GetIPandPort());
        }
    }
}
