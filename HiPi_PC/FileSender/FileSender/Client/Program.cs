using System;
using Client;

namespace Client
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
                    using (var client = new Client())
                    {
                        Console.WriteLine("Client started...");
                        Console.Write("Write in IP: ");
                        ip = Console.ReadLine();
                        client.SetIp(ip);
                        Console.Write("Write in PORT: ");
                        port = Convert.ToInt32(Console.ReadLine());
                        client.SetPort(port);
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
