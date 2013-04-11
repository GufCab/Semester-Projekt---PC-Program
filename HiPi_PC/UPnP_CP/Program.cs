using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenSource.DeviceBuilder;
using OpenSource.UPnP;

namespace UPnP_CP
{
    class Program
    {
        public static MediaRendererDiscovery SinkDisco;
        public static MediaServerDiscovery SourceDisco;

        public static SinkStack.CpAVTransport _AVTransport;
        public static SinkStack.CpConnectionManager _ConnectionManager;
        public static SinkStack.CpRenderingControl _RenderingControl;

        public static bool i = false;

        [STAThread]
        static void Main(string[] args)
        {
            System.Console.WriteLine("UPnP .NET Framework Stack");
            System.Console.WriteLine("StackBuilder Build#Device Builder Build#1.0.4144.25068");


            SinkDisco = new MediaRendererDiscovery();
            SinkDisco.OnAddedDevice += new MediaRendererDiscovery.DiscoveryHandler(AddSink);
            SinkDisco.OnRemovedDevice += new MediaRendererDiscovery.DiscoveryHandler(RemoveSink);

            /*
            SourceDisco = new MediaServerDiscovery();
            SourceDisco.OnAddedDevice += new MediaServerDiscovery.DiscoveryHandler(AddSource);
            SourceDisco.OnRemovedDevice += new MediaServerDiscovery.DiscoveryHandler(RemoveSource);
            */

            SinkDisco.Start();
            //SourceDisco.Start();

            while (i == false)
            {
                
            }

            while (true)
            {
                Console.WriteLine("Press button to send AV next()");
                Console.ReadLine();
                _AVTransport.Next(1);
            }

            System.Console.ReadLine();
        }

        private static void AddSink(MediaRendererDiscovery sender, UPnPDevice d)
        {
            Console.WriteLine("Added Device: " + d.FriendlyName);

            _AVTransport = new SinkStack.CpAVTransport(d.GetServices(SinkStack.CpAVTransport.SERVICE_NAME)[0]);

            i = true;
        }

        private static void RemoveSink(MediaRendererDiscovery sender, UPnPDevice d)
        {
            Console.WriteLine("Removed Device: " + d.FriendlyName);
        }


        private static void AddSource(MediaServerDiscovery sender, UPnPDevice dev)
        {
            //Todo: Implement Stack reception with wrappers
            throw new NotImplementedException();
        }

        private static void RemoveSource(MediaServerDiscovery sender, UPnPDevice dev)
        {
            //Todo: Remove device in some way
            throw new NotImplementedException();
        }
    }
}
