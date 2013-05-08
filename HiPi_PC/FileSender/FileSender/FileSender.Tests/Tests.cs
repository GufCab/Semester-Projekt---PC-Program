using System;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
<<<<<<< HEAD
=======
using System.Net.Sockets;
>>>>>>> 1c53e626a0f85abbb20dc48e47e380b75bf29278
=======
using System.Net.Sockets;
>>>>>>> 095316f42b7332c79035998705605e2e130bec1f
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Rhino.Mocks;
<<<<<<< HEAD
<<<<<<< HEAD
=======
using Server;
using Client;
>>>>>>> 1c53e626a0f85abbb20dc48e47e380b75bf29278
=======
using Server;
using Client;
>>>>>>> 095316f42b7332c79035998705605e2e130bec1f

namespace FileSender.Tests
{
    [TestFixture]
    public class ClientTests
    {
        [Test]
<<<<<<< HEAD
<<<<<<< HEAD
        public void Client_Constructor_SetUpIsCalled()
        {
            
        }
=======
=======
>>>>>>> 095316f42b7332c79035998705605e2e130bec1f
        public void Client_SetIp_SetIpIsCalled()
        {
            //Arrange
            var client = MockRepository.GenerateMock<IClient>();

            //Act
            client.SetIp(Arg<string>.Is.Anything);

            //Assert
            client.AssertWasCalled(x => x.SetIp("10.193.7.239"));
        }

        [Test]
        public void Client_SetIp_IpIsSetTo_10_192_7_239()
        {
            //Arrange
            var client = new Client.Client();
            const string expected = "10.193.7.239";

            //Act
            client.SetIp("10.193.7.239");

            //Assert
            Assert.AreEqual(expected, client._ip);
        }

        [Test]
        public void Client_SetPort_SetPortIsCalled()
        {
            //Arrange
            var client = MockRepository.GenerateMock<IClient>();

            //Act
            client.SetPort(Arg<int>.Is.Anything);

            //Assert
            client.AssertWasCalled(x => x.SetPort(9000));
        }

        [Test]
        public void Client_SetPort_PortIsSetTo9000()
        {
            //Arrange
            var client = new Client.Client();
            const int expected = 9000;

            //Act
            client.SetPort(9000);

            //Assert
            Assert.AreEqual(expected, client._port);
        }

        [Test]
        public void Client_SetFileName_SetFileNameIsCalled()
        {
            //Arrange
            var client = MockRepository.GenerateMock<IClient>();

            //Act
            client.SetFileName(Arg<string>.Is.Anything);

            //Assert
            client.AssertWasCalled(x => x.SetFileName("Jump.mp3"));
        }

        [Test]
        public void Client_SetFileName_FileNameIsSetTo_Jump_mp3()
        {
            //Arrange
            var client = new Client.Client();
            const string expected = "Jump.mp3";

            //Act
            client.SetFileName("Jump.mp3");

            //Assert
            Assert.AreEqual(expected, client._fileName);
        }

        //Not tested as 'NetworkStream', 'string' and 'int' doesn't have an interface
        [Test]
        [Ignore]
        public void Client_SendFile_SendFileIsCalled()
        {
            //Arrange
            var client = MockRepository.GenerateMock<IClient>();
            var networkStream = MockRepository.GenerateMock<NetworkStream>();
            var fileName = MockRepository.GeneratePartialMock<string>();
            var fileSize = MockRepository.GeneratePartialMock<int>();

            //Act
            client.SendFile(fileName, fileSize, networkStream);

            //Assert
            client.AssertWasCalled(x => x.SendFile(Arg<string>.Is.Anything, Arg<Int64>.Is.Anything, Arg<NetworkStream>.Is.Anything));
        }

        [Test]
        public void Client_SetUp_SetUpIsCalled()
        {
            //Arrange
            var client = MockRepository.GenerateMock<IClient>();
            client.SetIp("10.193.7.239");
            client.SetPort(9000);

            //Act
            client.SetUp();


            //Assert
            client.AssertWasCalled(x => x.SetUp());
        }

        [Test]
        public void Client_Dispose_DisposeIsCalled()
        {
            //Arrange
            var client = MockRepository.GenerateMock<IClient>();

            //Act
            client.Dispose();

            //Assert
            client.AssertWasCalled(x => x.Dispose());
        }

        [Test]
        public void Client_CloseSocket_CloseSocketIsCalled()
        {
            //Arrange
            var client = MockRepository.GenerateMock<IClient>();

            //Act
            client.CloseSocket();

            //Assert
            client.AssertWasCalled(x => x.CloseSocket());
        }
    }

    [TestFixture]
    public class ServerTests
    {
        //Tests need to be written
<<<<<<< HEAD
>>>>>>> 1c53e626a0f85abbb20dc48e47e380b75bf29278
=======
>>>>>>> 095316f42b7332c79035998705605e2e130bec1f
    }
}
