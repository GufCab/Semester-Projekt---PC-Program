using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaReader.FileIndexer;
using MetaReader.MetadataReader;

namespace MetadataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var index = new FolderAndFileReader();
            index.SetIndexPath(@"C:\Users\Public\Music\Sample Music");

            List<IMetadataReader> hans = index.GetMetaData();

            Console.WriteLine("Testing ItemName");
            foreach (var metadataReader in hans)
            {
                
                Console.WriteLine(metadataReader.ItemName);
            }
            Console.WriteLine("Testing Title");
            foreach (var metadataReader in hans)
            {
                
                Console.WriteLine(metadataReader.Title); 
               
            }
        }
    }
}
