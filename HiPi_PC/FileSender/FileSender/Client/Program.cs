using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var client = new Client(args);
                Console.WriteLine("Client started...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
