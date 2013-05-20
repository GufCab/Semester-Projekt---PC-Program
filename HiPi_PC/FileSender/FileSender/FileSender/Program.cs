using System;
using Client;

namespace FileSender
{
    public class Program
    {
        private static string ip;
        private static int port;
        private static string fileName;

        static void Main(string[] args)
        {
            try
            {
                do
                {
                    using (var client = new Client("10.193.7.239"))
                    {
                            Console.WriteLine("Client started...");
                            client.SetUp();
                            Console.Write("Set file to send: ");
                            fileName = Console.ReadLine();
                            client.SetFileName(@fileName);
                            Console.WriteLine();
                            client.SendFile(client._fileName, Convert.ToInt32(client._fileSize), client._serverStream);
                            client.CloseSocket();
                    }
                } while (true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
