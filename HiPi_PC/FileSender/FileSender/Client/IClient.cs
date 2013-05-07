using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public interface IClient
    {
        void SetUp();
        void SetIp(string ip);
        void SetPort(int port);
        void SetFileName(string fileName);
        void CloseSocket();
        void SendFile(String fileName, long fileSize, NetworkStream io);
        void Dispose();
    }
}
