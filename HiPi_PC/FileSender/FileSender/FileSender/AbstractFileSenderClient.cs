using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FileSender
{
    public abstract class AbstractFileSenderClient
    {
        protected abstract string LocalIpAddress();
        protected abstract void SetUp();
        protected abstract void SendFileNameToServer();
        protected abstract void SetIp(string ip);
        protected abstract void SetPort(int port);
        protected abstract void SetFileName(string fileName);
        protected abstract void SendFileSizeToServer();
        protected abstract void CloseSocket();
        protected abstract void SendFile(String fileName, long fileSize, NetworkStream io);
        public abstract void Dispose();
        protected abstract void Run(string path);
    }
}
