using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;

namespace FileSender
{

    public class Client : IDisposable
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

        private void SetUp()
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

        //For test-purpose
        public Client(string fileName)
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

        public Client(string fileName, string ip)
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

        private void SendFileNameToServer()
        {
            fileInfo = new FileInfo(_fileName);
            LIB.writeTextTCP(_serverStream, serverFileName ?? "Given file not found!");                //Write file name to server
        }

        private void SetIp(string ip)
        {
            _ip = ip;
        }

        private void SetPort(int port)
        {
            _port = port;
        }

        private void SetFileName(string fileName)
        {
            _fileName = fileName;
            serverFileName = Path.GetFileName(_fileName);
        }

        private void SendFileSizeToServer()
        {
            _fileSize = fileInfo.Length;
            LIB.writeTextTCP(_serverStream, _fileSize.ToString());                              //Write file size to server
        }

        private void CloseSocket()
        {
            _clientSocket.Close();
        }

        private void SendFile(String fileName, long fileSize, NetworkStream io)
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

        public void Dispose()
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

        private void Run(string path)
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
