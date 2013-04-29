using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using tcp;

namespace Client
{
    class Client
    {
        /// <summary>
        /// The PORT.
        /// </summary>
        const int PORT = 9000;
        /// <summary>
        /// The BUFSIZE.
        /// </summary>
        const int BUFSIZE = 10000;

        private TcpClient _clientSocket;
        private NetworkStream _serverStream;
		private long _fileSize;
        private string _fileName;
        private FileInfo fileInfo;
        private string[] allArgs;

        private void SetUp()
        {
            try
            {
                _clientSocket = new TcpClient();															            //Create and initialize TCPClient
                _clientSocket.Connect(allArgs[0], PORT);													           //Connect to server
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}, {1}", e.Source, e);
                throw;
            }

        }
        public Client(string[] args)
        {
            try
            {
                allArgs = args;
                SetUp();
                Console.WriteLine(" >> TCP client started - connected to {0} on port {1}...", args[0], PORT);	    //Tell user that client is started on chosen port
                _serverStream = _clientSocket.GetStream();															//Get the stream path to the server
                _fileName = args[1];
                fileInfo = new FileInfo(_fileName);
                LIB.writeTextTCP(_serverStream, (args != null) ? _fileName : "No file-path found!");                  //Write file name to server
                _fileSize = fileInfo.Length;
                LIB.writeTextTCP(_serverStream, _fileSize.ToString());                                              //Write file size to server
                SendFile(_fileName, Convert.ToInt32(_fileSize), _serverStream);
                _clientSocket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void SendFile(String fileName, long fileSize, NetworkStream io)
        {
            byte[] fileData;
            // To do your code

            //Åben en fileStream
            //Sæt variabler
            //Send 1000 bytes indtil
            try
            {
                FileStream openFileStream = new FileStream(fileName, FileMode.OpenOrCreate);
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

                bReader.Close();
                openFileStream.Close();
            }
            catch (Exception)
            {
                throw new FileNotFoundException("File {0} not found!", fileName);
            }
        }
    }
}
