using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using tcp;

namespace Server
{
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    public class Server : IDisposable
=======
    public class Server : IServer, IDisposable
>>>>>>> 1c53e626a0f85abbb20dc48e47e380b75bf29278
=======
    public class Server : IServer, IDisposable
>>>>>>> 095316f42b7332c79035998705605e2e130bec1f
=======
    public class Server : IServer, IDisposable
>>>>>>> 095316f42b7332c79035998705605e2e130bec1f
=======
    public class Server : IServer, IDisposable
>>>>>>> 095316f42b7332c79035998705605e2e130bec1f
=======
    public class Server : IServer, IDisposable
>>>>>>> e55acdce42a7bcd42d9c2dd53de457e0db586ded
    {
        public IPAddress _IP { get; private set; }
        public TcpListener _serverSocket { get; private set; }
        public TcpClient _clientSocket { get; private set; }
        public NetworkStream _serverStream { get; private set; }
        public string _fileName { get; private set; }
        public string _fileSize { get; private set; }

        private string LocalIpAddress()
        {
            string localIp = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp = ip.ToString();
                }
            }
            return localIp;
        }

        public void SetUp()
        {
            try
            {
                _IP = IPAddress.Parse(LocalIpAddress());                                            //Convert tempIP to IP
                _serverSocket = new TcpListener(_IP, PORT);                                         //Create and initialize TCPlistener
                _clientSocket = new TcpClient();
                _clientSocket = default(TcpClient);
                _serverSocket.Start();                                                              //Start listening on serverSocket
                Console.WriteLine(" >> TCP server started - Listening on port {0}...", PORT);       //Write which port we're listening to
                Console.WriteLine(" >> The IP:Port is: {0}:{1}", LocalIpAddress(), PORT);           //Write local end point
                Console.WriteLine(" >> Waiting for connection.....");                               //Indicate server is waiting for connection
                Console.WriteLine();
                _clientSocket = _serverSocket.AcceptTcpClient();                                    //Set server to accept connections
                _serverStream = _clientSocket.GetStream();                                          //Prepare to receive file name
                if (_clientSocket != null)
                {
                    Console.WriteLine(" >> TCP server connected to {0} on port {1}...",
                                        _clientSocket.Client.LocalEndPoint, PORT);                    //Tell user that client is started on chosen port
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} happened!", e.Data);
                throw;
            }
        }
        
        public void ReadFileName()
        {
            _fileName = Path.GetFileName(LIB.readTextTCP(_serverStream)); //Read file name
            Console.WriteLine("File name is: '{0}'", Path.GetFileName(_fileName)); //Tell the user the file name
        }

        public void ReadFileSize()
        {
            _fileSize = LIB.readTextTCP(_serverStream); //Read file size
            Console.WriteLine("File size is: {0} bytes.", _fileSize); //Output file size to console
        }

        /// <summary>
        /// The PORT
        /// </summary>
        public const int PORT = 9003;
        /// <summary>
        /// The BUFSIZE
        /// </summary>
        public const int BUFSIZE = 10000;

        public void ReceiveFile(String fileName, NetworkStream io)
        {
            // TO DO Din egen kode
            byte[] fileData = new byte[BUFSIZE];

            FileStream writeFileStream = new FileStream(LIB.extractFileName(fileName), FileMode.Create);
            BinaryWriter bWrite = new BinaryWriter(writeFileStream);

            int bytesRead = 0;
            long remainingSize = Convert.ToInt32(_fileSize);

            do
            {
                Console.WriteLine("Remaining number of bytes: {0}", remainingSize);
                bytesRead = io.Read(fileData, 0, BUFSIZE); // Read max 1000 bytes from server via socket (actual value is placed in "bytesRead"
                bWrite.Write(fileData, 0, bytesRead); // write the received bytes into file. the number of received bytes is placed in "bytesRead"
                remainingSize -= bytesRead;
            }
            while (remainingSize > 0);

            writeFileStream.Flush();
            writeFileStream.Close();
            bWrite.Close();
        }

        public void CloseSocketConnection()
        {
            _clientSocket.Close();
        }

        public void Dispose()
        {
            _serverSocket.Stop();
            _clientSocket.Close();
        }
    }
}
