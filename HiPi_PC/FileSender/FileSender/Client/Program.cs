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
<<<<<<< HEAD
<<<<<<< HEAD
                    using (var client = new Client())
                    {
                        Console.WriteLine("Client started...");
                        Console.Write("Write in IP: ");
                        ip = Console.ReadLine();
                        client.SetIp(ip);
                        Console.Write("Write in PORT: ");
                        port = Convert.ToInt32(Console.ReadLine());
                        client.SetPort(port);
=======
                    using (var client = new Client("10.193.7.239"))
                    {
                        Console.WriteLine("Client started...");
>>>>>>> e55acdce42a7bcd42d9c2dd53de457e0db586ded
=======
                    using (var client = new Client("10.193.7.239"))
                    {
                        Console.WriteLine("Client started...");
>>>>>>> e55acdce42a7bcd42d9c2dd53de457e0db586ded
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
