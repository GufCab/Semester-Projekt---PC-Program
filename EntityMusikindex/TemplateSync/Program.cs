using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateSync
{
    class Program
    {
        static void Main(string[] args)
        {
            var sync = new SyncTemplate();

            List<string> ha = new List<string>();
            ha.Add(@"C:\Users\Public\Music");


            sync.Sync(ha);

        }
    }
}
