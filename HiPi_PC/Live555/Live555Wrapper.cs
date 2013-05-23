using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Live555
{
   public class Live555Wrapper
    {
        private Process _liveServer;
        private StreamReader _outStream;
        private StreamWriter _inStream;
        private string IP= "If you're reading this, something went wrong";

        public Live555Wrapper()
        {
            Live555Setup();
        }

        private void Live555Setup()
        {
            try
            {
                IPHostEntry host;
                string localIP = "?";
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        IP = localIP;
                    }
                }

                _liveServer = new Process
                    {
                        StartInfo = new ProcessStartInfo
                            {
                                FileName = "live555MediaServer",
                                UseShellExecute = false,
                                RedirectStandardOutput = true //,
                                //CreateNoWindow = true
                            }
                    };

                _liveServer.Start();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public string GetIP()
        {
            return IP;
        }
        public string GetIPandPort()
        {
            string tempIP = IP + ":8554";
            return tempIP;
        }
    }
}
