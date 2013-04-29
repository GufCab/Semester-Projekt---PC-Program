using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lårt
{
    class Program
    {
        static void Main(string[] args)
        {



            Uri to = new Uri(@"C:\Users\Public\Music");
            // Must end in a slash to indicate folder
            Uri from = new Uri(Environment.CurrentDirectory);
            string relativePath =
            Uri.UnescapeDataString(
                from.MakeRelativeUri(to)
                    .ToString()
                    .Replace('/', Path.DirectorySeparatorChar)
                );
            Console.WriteLine("From Current: "+ Environment.CurrentDirectory);
            Console.WriteLine(@"to music: C:\Users\Public\Music");
            Console.WriteLine(relativePath);
        }
    }
}
