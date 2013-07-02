using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This class starts the live555MediaServer in a separate process and saves the IP and Port to stream from, which can be returned with the GetIP() and GetIPandPort() funtions.
/// </summary>
namespace Live555
{
   /// <summary>
   /// This class starts the live555MediaServer in a separate process and saves the IP and Port to stream from, which can be returned with the GetIP() and GetIPandPort() funtions.
   /// </summary>
   public class Live555Wrapper
    {
        private Process _liveServer;
        private StreamReader _outStream;
        private StreamWriter _inStream;
        private string IP= "If you're reading this, something went wrong";

        /// <summary>
        /// Constructor
        /// </summary>
        public Live555Wrapper()
        {
            Live555Setup();
        }

        /// <summary>
        /// Starts the live555MediaServer in a new process and saves the IP and Port to stream from.
        /// </summary>
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
                                RedirectStandardOutput = true
                            }
                    };

                _liveServer.Start();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returns IP of host device.
        /// </summary>
        /// <returns>IP of host device</returns>
        public string GetIP()
        {
            return IP;
        }

        /// <summary>
        /// Returns IP of host device and Port used for streaming.
        /// </summary>
        /// <returns> IP and Port to stream from</returns>
        public string GetIPandPort()
        {
            string tempIP = IP + ":8554";
            return tempIP;
        }
    }
}
