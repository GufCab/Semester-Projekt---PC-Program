using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
        [Test]
        public void Client_Run_RunIsCalled()
        {
            //Arrange
            var client = MockRepository.GenerateMock<IClient>();
            FileInfo expected = new FileInfo("D:\\PTC.Mathcad.Prime.v2.0.X64-Lz0.zip");

            //Act
            client.Run("D:\\PTC.Mathcad.Prime.v2.0.X64-Lz0.zip");
            FileInfo actual =
                new FileInfo(
                    "C:\\Users\\Michael O\\Documents\\GitHub\\Semester-Projekt---PC-Program\\HiPi_PC\\FileSender\\FileSender\\Server\\bin\\Debug\\PTC.Mathcad.Prime.v2.0.X64-Lz0.zip");

            //Assert
            FileAssert.AreEqual(expected, actual);
        }
    }

    [Ignore]
    [TestFixture]
    public class ServerTests
    {
        //Tests need to be written
    }
}
