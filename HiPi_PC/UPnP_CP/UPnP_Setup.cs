using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenSource.DeviceBuilder;
using OpenSource.UPnP;

namespace UPnP_CP
{
    public class UPnP_Setup
    {
        private static MediaRendererDiscovery _SinkDisco;
        private static MediaServerDiscovery _SourceDisco;
        /*
        public static SinkStack.CpAVTransport _AVTransport;
        public static SinkStack.CpConnectionManager _ConnectionManager;
        public static SinkStack.CpRenderingControl _RenderingControl;
        */



        public static void StartSourceDisco()
        {
            _SourceDisco = new MediaServerDiscovery();
            _SourceDisco.OnAddedDevice += new MediaServerDiscovery.DiscoveryHandler(AddSource);
            _SourceDisco.OnRemovedDevice += new MediaServerDiscovery.DiscoveryHandler(RemoveSource);
            _SourceDisco.Start();
        }

        public static void StartSinkDisco()
        {
            _SinkDisco = new MediaRendererDiscovery();
            _SinkDisco.OnAddedDevice += new MediaRendererDiscovery.DiscoveryHandler(AddSink);
            _SinkDisco.OnRemovedDevice += new MediaRendererDiscovery.DiscoveryHandler(RemoveSink);
            _SinkDisco.Start();

        }

        private static void AddSink(MediaRendererDiscovery sender, UPnPDevice d)
        {
            Console.WriteLine("Added Device: " + d.FriendlyName);

                UPnP_Functions._AVTransport = new SinkStack.CpAVTransport(d.GetServices(SinkStack.CpAVTransport.SERVICE_NAME)[0]);
                UPnP_Functions._ConnectionManager = new SinkStack.CpConnectionManager(d.GetServices(SinkStack.CpConnectionManager.SERVICE_NAME)[0]);
                UPnP_Functions._RenderingControl = new SinkStack.CpRenderingControl(d.GetServices(SinkStack.CpRenderingControl.SERVICE_NAME)[0]);
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
