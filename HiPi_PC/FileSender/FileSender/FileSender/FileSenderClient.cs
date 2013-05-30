using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;

namespace FileSender
{
    /// <summary>
    /// This class is responsible for handling the task of sending files from the Client (The PC) to the Server (The Raspberry Pi).
    /// </summary>
    public class FileSenderClient : AbstractFileSenderClient, IDisposable
    {
        /// <summary>
        /// The BUFSIZE.
        /// </summary>
        const int BUFSIZE = 1000;

        public TcpClient _clientSocket { get; private set; }
        public NetworkStream _serverStream { get; private set; }
        public long _fileSize { get; private set; }
        public string _fileName { get; private set; }
        private FileInfo fileInfo;
        public string _ip { get; private set; }
        public int _port { get; private set; }
        private string serverFileName;

        /// <summary>
        /// This method gets an array of the IP-addresses that the system uses.
        /// </summary>
        /// <returns>
        /// A string containing an IP.
        /// </returns>
        protected override string LocalIpAddress()
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

        /// <summary>
        /// This method is responsible for setting up the required variables and external classes.
        /// </summary>
        protected override void SetUp()
        {

            _clientSocket = new TcpClient(); //Create and initialize TCPClient

            try
            {
                _clientSocket.Connect(_ip, _port); //Connect to server
                _serverStream = _clientSocket.GetStream(); //Get the stream path to the server
            }
            catch (Exception)
            {
                throw new ArgumentException("Ip or port is not accessible so no connection to server is possible!");
            }

            if (_clientSocket != null)
            {
                Console.WriteLine(" >> TCP client started - connected to {0} on port {1}...", _ip, _port);  //Tell user that client is started on chosen port
            }


        }

        /// <summary>
        /// This constructor is purely for testing purpose.
        /// This constructor sets the port that it should connect to and the file it should send.
        /// </summary>
        /// <param name="fileName">The file name of the file-to-send</param>
        public FileSenderClient(string fileName)
        {
            try
            {
                SetPort(9003);
                Run(fileName);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// This constructor sets the IP and port it should connect to and it sets the file name of the file-to-send.
        /// </summary>
        /// <param name="fileName">The file name of the file-to-send</param>
        /// <param name="ip">The IP of the receiver</param>
        public FileSenderClient(string fileName, string ip)
        {
            try
            {
                SetIp(ip);
                SetPort(9003);
                Run(fileName);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Sends the file name to the receiver.
        /// </summary>
        protected override void SendFileNameToServer()
        {
            fileInfo = new FileInfo(_fileName);
            LIB.writeTextTCP(_serverStream, serverFileName ?? "Given file not found!");                //Write file name to server
        }

        /// <summary>
        /// Sets the IP of the receiver.
        /// </summary>
        /// <param name="ip">IP of the receiver.</param>
        protected override void SetIp(string ip)
        {
            _ip = ip;
        }

        /// <summary>
        /// Sets the port that the system uses.
        /// </summary>
        /// <param name="port">Port to use.</param>
        protected override void SetPort(int port)
        {
            _port = port;
        }

        /// <summary>
        /// Sets the name of the file-to-send.
        /// </summary>
        /// <param name="fileName">Name of the file-to-send.</param>
        protected override void SetFileName(string fileName)
        {
            _fileName = fileName;
            serverFileName = Path.GetFileName(_fileName);
        }

        /// <summary>
        /// Sends the file size to the receiver.
        /// </summary>
        protected override void SendFileSizeToServer()
        {
            _fileSize = fileInfo.Length;
            LIB.writeTextTCP(_serverStream, _fileSize.ToString());                              //Write file size to server
        }

        /// <summary>
        /// Closes the connection socket of the client.
        /// </summary>
        protected override void CloseSocket()
        {
            _clientSocket.Close();
        }

        /// <summary>
        /// This method sends the file to the receiver through packets of bytes.
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="fileSize">Size of the file</param>
        /// <param name="io"></param>
        protected override void SendFile(String fileName, long fileSize, NetworkStream io)
        {
            SetFileName(fileName);
            SendFileNameToServer();
            SendFileSizeToServer();

            byte[] fileData;

            try
            {
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException("File does not exist!");
                }
                FileStream openFileStream = File.OpenRead(fileName);
                BinaryReader bReader = new BinaryReader(openFileStream);

                Int32 remainingSize = Convert.ToInt32(_fileSize);

                if (remainingSize > BUFSIZE)
                {
                    do
                    {
                        fileData = bReader.ReadBytes(BUFSIZE);
                        io.Write(fileData, 0, BUFSIZE);
                        remainingSize -= BUFSIZE;
                    } while (remainingSize > BUFSIZE);
                }

                if (remainingSize < BUFSIZE)
                {
                    do
                    {
                        fileData = bReader.ReadBytes(remainingSize);
                        io.Write(fileData, 0, remainingSize);
                        remainingSize -= remainingSize;
                    } while (remainingSize > 0);
                }

                openFileStream.Flush();
                bReader.Close();
                openFileStream.Close();
                io.Flush();
                io.Close();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Implemented so usage of "using(xxxxxx)" is possible. 
        /// </summary>
        public override void Dispose()
        {
            try
            {
                _clientSocket.Close();
                _serverStream.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Cannot close connections as they do not exist!");
                throw new Exception();
            }
        }

        /// <summary>
        /// Runs Sequentially through the process of sending the file as bytes.
        /// </summary>
        /// <param name="path">The path to the file, including the file name.</param>
        protected override void Run(string path)
        {
            try
            {
                if (_ip == null)
                {
                    var clientIp = LocalIpAddress(); //"10.193.12.93";
                    SetIp(clientIp);
                }
                Console.WriteLine("Client started...");
                SetUp();
                Console.Write("Set file to send: ");
                var fileName = path;
                SetFileName(@fileName);
                Console.WriteLine();
                SendFile(fileName, Convert.ToInt32(_fileSize), _serverStream);
                CloseSocket();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception();
            }
        }
    }
}
