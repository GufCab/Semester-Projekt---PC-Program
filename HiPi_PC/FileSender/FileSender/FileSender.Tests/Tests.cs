using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
using FileSender;

namespace FileSender.Tests
{
    [TestFixture]
    public class ClientTests
    {
        //[assembly: InternalsVisibleTo("Client.dll")]
        [Test]
        public void Client_Constructor_RunIsCalled()
        {
            //Arrange
            var client = new Client("D:\\PTC.Mathcad.Prime.v2.0.X64-Lz0.zip");

            //Assert
            
        }
    }

    [Ignore]
    [TestFixture]
    public class ServerTests
    {
        //Tests need to be written
    }
}
