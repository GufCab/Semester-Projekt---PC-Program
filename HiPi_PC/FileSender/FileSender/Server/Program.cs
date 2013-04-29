using System;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var server = new Server();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
