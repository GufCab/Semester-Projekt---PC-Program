using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaReader.FileIndexer;
using MetaReader.IOReader;
using MetaReader.MDataReader;
using MetaReader.MetadataReader;
using Shell32;


namespace MetaReader
{
    class Program
    {
        private static readonly string Newline = Environment.NewLine;

        static void Main()
        {
            const string musikFolder = @"C:\Users\Jakob\Music\";

            //var dataContainer = new Reader(@"C:\Users\Jakob\Music\temp\");
            //dataContainer.ReaderSetup(@"C:\Users\Jakob\Music\temp\");
            
            
            
            //=================
            // Next Test
            //=================
            testTre(musikFolder);

            //=================
            // Test FoldersAndFileReader
            //=================
            //FolderAndFileIndexerTest(musikFolder);
            
            //=================
            // Test stop
            //=================

            //int count = indexer.FolderItemList.Count - 1;

            //Console.WriteLine("Please enter a number of a valid song (0 - {0}):", count);
            //Int16 temp = Convert.ToInt16(Console.ReadLine());
            //dataContainer.PrintOne(temp);

            //dataContainer.AccessData();
            //dataContainer.PrintArrHeader();

            //==============
            // print all metadata:
            //==============

            //printMetaData(musikFolder);

            //dataContainer.PrintItem();
            //dataContainer.Print();
            //dataContainer.PrintTitle();
        }

        private static void printMetaData(string musikFolder)
        {
            List<string> arrHeaders = new List<string>();

            EntryText("Writing MetaData");

            Shell32.Shell _shell = new Shell();
            Shell32.Folder _objFolder = _shell.NameSpace(musikFolder);

            for (int i = 0; i < short.MaxValue; i++)
            {
                string header = _objFolder.GetDetailsOf(null, i);
                if (String.IsNullOrEmpty(header))
                    break;
                arrHeaders.Add(header);
            }


            foreach (var items in _objFolder.Items())
            {
                for (int i = 0; i < arrHeaders.Count; i++)
                {
                    Console.WriteLine("{0}\t{1}: {2}", i, arrHeaders[i], _objFolder.GetDetailsOf(items, i));
                }
                break;
            }
        }



        private static void testEt(string musikFolder)
        {
            //=============
            // Test the FileIndexer and MDataReader
            //=============
            var indexer = new FileIndexer.FolderAndFileReader();
            indexer.GetMetaData();

            foreach (var VARIABLE in indexer.FolderItemList)
            {
                Console.WriteLine(VARIABLE);
            }

            Console.WriteLine();
            Console.WriteLine("==================================");
            Console.WriteLine(" Testing the MDataReader - single ");
            Console.WriteLine("==================================");
            Console.WriteLine();

            foreach (var data in indexer.GetMetaData())
            {
                Console.WriteLine("-------------------------------------" +
                                  "\nTest for the item: {0}", data.ItemName);
                Console.WriteLine();

                if (data.Title != "")
                {
                    Console.Write(
                        data.Title + Newline +
                        data.Artist + Newline +
                        data.Album + Newline +
                        data.Nr + Newline +
                        data.Genre + Newline +
                        data.LengthS + Newline +
                        data.Filepath + Newline
                        );
                }
            }
        }

        private static void testTo(string musikFolder)
        { 
            EntryText("DirectoryReader Test");

            IOReader.DirectoryReader directoryReader = new DirectoryReader(musikFolder);
            directoryReader.PrintFilesAndFolders();

            EntryText("DirectoryReader Test 2");

            foreach (string afaf in directoryReader.AllFilesAndFolders)
            {
                Console.WriteLine(afaf);
            }
        }

        private static void FolderAndFileIndexerTest(string musikFolder)
        {
            
            EntryText("Test FoldersAndFileReader");
            FileIndexer.FolderAndFileReader folderAndFileReader = new FolderAndFileReader();
            
            folderAndFileReader.SetIndexPath(musikFolder);

            //folderAndFileReader.foldersVers_One();
            
            foreach (string foldersAndFile in folderAndFileReader.FoldersAndFiles)
            {
                Console.WriteLine(foldersAndFile);
            }

            //folderAndFileReader.TempPrint();
            
        }

        private static void testTre(string musikFolder)
        {
            EntryText(" Test FolderIndexer and MetaData");

            FileIndexer.IFileIndexer folderAndFileReader = new FolderAndFileReader();

            folderAndFileReader.SetIndexPath(musikFolder);

            List<IMetadataReader> metadata = folderAndFileReader.GetMetaData();

            
            EntryText(" Testing MetaData ");
            foreach (IMetadataReader data in metadata)
            {
                Console.WriteLine(data.ItemName);
                Console.WriteLine(data.Title);
                Console.WriteLine(data.Artist);
                Console.WriteLine(data.Filepath);
                Console.WriteLine(data.Lengthstring);
                Console.WriteLine(data.LengthS);
            }
        }
        private static void testFire()
        { }

        private static void WriteMetaData()
        {
            Console.WriteLine();
            Console.WriteLine("==================================");
            Console.WriteLine(" Writing Metadat ");
            Console.WriteLine("==================================");
            Console.WriteLine();

            MetaReader tempra = new MetaReader();
            tempra.DataReaderSetup("yrmp");
        }

        private static void EntryText(string text)
        {
            Console.WriteLine();
            Console.WriteLine("==================================");
            Console.WriteLine(text);
            Console.WriteLine("==================================");
            Console.WriteLine();
        }
    }
}
