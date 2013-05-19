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

        //private UPnP_SinkFunctions Sink;

        public delegate void AddSinkHandler(UPnP_SinkFunctions e, EventArgs s);
        public event AddSinkHandler AddSinkEvent;

        public delegate void AddSourceHandler(UPnP_SourceFunctions e, EventArgs s);
        public event AddSourceHandler AddSourceEvent;

        public delegate void RemoveSourceHandler(object e, EventArgs s);
        public event RemoveSourceHandler RemoveSourceEvent;

        
        public void StartSourceDisco()
        {
            _SourceDisco = new MediaServerDiscovery();
            _SourceDisco.OnAddedDevice += new MediaServerDiscovery.DiscoveryHandler(AddSource);
            _SourceDisco.OnRemovedDevice += new MediaServerDiscovery.DiscoveryHandler(RemoveSource);
            _SourceDisco.Start();
        }

        public void StartSinkDisco()
        {
            _SinkDisco = new MediaRendererDiscovery();
            _SinkDisco.OnAddedDevice += new MediaRendererDiscovery.DiscoveryHandler(AddSink);
            _SinkDisco.OnRemovedDevice += new MediaRendererDiscovery.DiscoveryHandler(RemoveSink);
            _SinkDisco.Start();
        }

        //removed "static"
        private void AddSink(MediaRendererDiscovery sender, UPnPDevice d)
        {
            Console.WriteLine("Added Sink Device: " + d.FriendlyName);
            if (d.FriendlyName == "HiPi - Sink")
            {
                UPnP_SinkFunctions func = new UPnP_SinkFunctions(
                    new SinkStack.CpAVTransport(d.GetServices(SinkStack.CpAVTransport.SERVICE_NAME)[0]), null,
                    //new SinkStack.CpConnectionManager(d.GetServices(SinkStack.CpConnectionManager.SERVICE_NAME)[0]),
                    new SinkStack.CpRenderingControl(d.GetServices(SinkStack.CpRenderingControl.SERVICE_NAME)[0]));

                AddSinkEvent(func, null);
            }
        }

        private static void RemoveSink(MediaRendererDiscovery sender, UPnPDevice d)
        {
            Console.WriteLine("Removed Device: " + d.FriendlyName);
            //Todo: Remove device somehow
        }

        //removed "static"
        private void AddSource(MediaServerDiscovery sender, UPnPDevice d)
        {
            Console.WriteLine("Added Source Device: " + d.FriendlyName);

            if (d.FriendlyName == "HiPi - Source")
            {
                UPnP_SourceFunctions func = new UPnP_SourceFunctions(null,
                    //new SourceStack.CpConnectionManager(d.GetServices(SourceStack.CpConnectionManager.SERVICE_NAME)[0]),
                    new SourceStack.CpContentDirectory(d.GetServices(SourceStack.CpContentDirectory.SERVICE_NAME)[0]));

                AddSourceEvent(func, null);
            }
        }

        private void RemoveSource(MediaServerDiscovery sender, UPnPDevice d)
        {
            Console.WriteLine("Device removed");
            RemoveSourceEvent(null, null);
        }


    }
}
