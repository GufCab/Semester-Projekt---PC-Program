using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;
using tcp;

namespace Client
{
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    class Client : IDisposable
=======
    public class Client : IClient, IDisposable
>>>>>>> 1c53e626a0f85abbb20dc48e47e380b75bf29278
=======
    public class Client : IClient, IDisposable
>>>>>>> 095316f42b7332c79035998705605e2e130bec1f
=======
    public class Client : IClient, IDisposable
>>>>>>> 095316f42b7332c79035998705605e2e130bec1f
=======
    public class Client : IClient, IDisposable
>>>>>>> 095316f42b7332c79035998705605e2e130bec1f
=======
    public class Client : IClient, IDisposable
>>>>>>> e55acdce42a7bcd42d9c2dd53de457e0db586ded
    {
        /// <summary>
        /// The BUFSIZE.
        /// </summary>
        const int BUFSIZE = 10000;

        public TcpClient _clientSocket { get; private set; }
        public NetworkStream _serverStream { get; private set; }
        public long _fileSize { get; private set; }
        public string _fileName { get; private set; }
        private FileInfo fileInfo;
        public string _ip { get; private set; }
        public int _port { get; private set; }
        private string serverFileName;

        public void SetUp()
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
            

<<<<<<< HEAD
        }

        public void SetIp(string ip)
        {
            _ip = ip;
        }

        public void SetPort(int port)
        {
            _port = port;
        }

        public void SetFileName(string fileName)
        {
            _fileName = fileName;
            serverFileName = Path.GetFileName(_fileName);
            Console.WriteLine(Path.GetFullPath(_fileName));
        }

        private void SendFileNameToServer()
        {
            fileInfo = new FileInfo(_fileName);
            LIB.writeTextTCP(_serverStream, serverFileName ?? "No file-path found!");                //Write file name to server
        }

        private void SendFileSizeToServer()
        {
=======
        }

        public Client(string ip)
        {
            SetIp(ip);
            SetPort(9003);
        }

        public void SetIp(string ip)
        {
            _ip = ip;
        }

        public void SetPort(int port)
        {
            _port = port;
        }

        public void SetFileName(string fileName)
        {
            _fileName = fileName;
            serverFileName = Path.GetFileName(_fileName);
        }

        private void SendFileNameToServer()
        {
            fileInfo = new FileInfo(_fileName);
            LIB.writeTextTCP(_serverStream, serverFileName ?? "Given file not found!");                //Write file name to server
        }

        private void SendFileSizeToServer()
        {
>>>>>>> e55acdce42a7bcd42d9c2dd53de457e0db586ded
            _fileSize = fileInfo.Length;
            LIB.writeTextTCP(_serverStream, _fileSize.ToString());                              //Write file size to server
        }

        public void CloseSocket()
        {
            _clientSocket.Close();
        }

        public void SendFile(String fileName, long fileSize, NetworkStream io)
        {
<<<<<<< HEAD
=======
            SetFileName(fileName);
>>>>>>> e55acdce42a7bcd42d9c2dd53de457e0db586ded
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


                do
                {
                    fileData = bReader.ReadBytes(BUFSIZE);
                    io.Write(fileData, 0, BUFSIZE);
                    remainingSize -= BUFSIZE;
                } while (remainingSize > BUFSIZE);

                do
                {
                    fileData = bReader.ReadBytes(remainingSize);
                    io.Write(fileData, 0, remainingSize);
                    remainingSize -= remainingSize;
                } while (remainingSize > 0);

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
            }
        }
    }
}
