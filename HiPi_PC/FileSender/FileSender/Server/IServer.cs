using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public interface IServer
    {
        void SetUp();
        void ReadFileName();
        void ReadFileSize();
        void ReceiveFile(String fileName, NetworkStream io);
        void CloseSocketConnection();
        void Dispose();
    }
}
