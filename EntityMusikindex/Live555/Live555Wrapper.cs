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

        private void Live555Setup()
        {
            _liveServer = new Process();
            var startInfo = new ProcessStartInfo();

            string arguments = "-idle -quiet -slave ";

            startInfo.FileName = "live555MediaServer.exe";
            startInfo.Arguments = arguments;

            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;

            startInfo.UseShellExecute = false;

            _liveServer.StartInfo = startInfo;

            //begin mplayer in idle
            _liveServer.Start();

            _outStream = _liveServer.StandardOutput;
            _inStream = _liveServer.StandardInput;
        }
    }
}
