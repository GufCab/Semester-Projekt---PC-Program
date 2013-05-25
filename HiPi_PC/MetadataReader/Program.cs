using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetadataReader.FileIndexer;
using MetadataReader.Metadata;

namespace MetadataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var index = new FolderAndFileReader();
            index.SetIndexPath(@"D:\Musik");

            List<IMetadataReader> hans = index.GetMetaData();

            Console.WriteLine(index.Folderpath);
            Console.WriteLine("Testing ItemName");
            foreach (var metadataReader in hans)
            {
                Console.WriteLine(metadataReader.Filepath);
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
