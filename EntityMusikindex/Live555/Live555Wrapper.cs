using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live555
{
    class Live555Wrapper
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
            //var SW = new StringWriter();
            _liveServer = new Process
                {
                    StartInfo = new ProcessStartInfo
                        {
                            FileName = "live555MediaServer",
                            UseShellExecute = false,
                            RedirectStandardOutput = true//,
                            //CreateNoWindow = true
                        }
                };
            //Console.SetOut(SW);
            //string arguments = "-idle -quiet -slave ";
            //startInfo.Arguments = arguments;

            
            //startInfo.RedirectStandardError = true;
            //startInfo.UseShellExecute = false;
            //startInfo.RedirectStandardInput = true;
            //startInfo.RedirectStandardOutput = true;

            _liveServer.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler);

            //_liveServer.StartInfo = startInfo;

            _liveServer.Start();
            _liveServer.BeginOutputReadLine();
            

            //string[] stringSeperators = new string[] { "/" };
            //while (!_liveServer.StandardOutput.EndOfStream)
            //{
            //    string line = _liveServer.StandardOutput.ReadLine();
            //    if (line.Contains("rtsp"))
            //    {
            //        string[] phrase = line.Split(stringSeperators, StringSplitOptions.RemoveEmptyEntries);
            //        IP = phrase[2];
            //    }
            //}
            //Console.WriteLine(SW.ToString());

        }

        public string GetIP()
        {
            return IP;
        }

        private void SortOutputHandler(object senderObject, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                if (outLine.Data.Contains("rtsp"))
                {
                    string[] stringSeperators = new string[] { "/" };
                    string[] phrase = outLine.Data.Split(stringSeperators, StringSplitOptions.RemoveEmptyEntries);
                    IP = phrase[2];
                }
            }
        }
    }
}
