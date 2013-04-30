using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaReader.FileIndexer;
using MetaReader.IOReader;
using MetaReader.MDataReader;


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
            
            //=================
            // Test FoldersAndFileReader
            //=================
            FolderAndFileIndexerTest(musikFolder);
            
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

            //WriteMetaData();

            //dataContainer.PrintItem();
            //dataContainer.Print();
            //dataContainer.PrintTitle();
        }

        private static void testEt(string musikFolder)
        {
            //=============
            // Test the FileIndexer and MDataReader
            //=============
            var indexer = new FileIndexer.FileIndexer(musikFolder);
            indexer.GetItems();

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

        private static void testTre()
        { }
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
