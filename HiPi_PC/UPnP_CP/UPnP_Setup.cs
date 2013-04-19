﻿using System;
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

        private UPnP_SinkFunctions Sink;


        public delegate void AddSinkHandler(UPnP_SinkFunctions e, EventArgs s);

        public event AddSinkHandler AddSinkEvent;

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
            Console.WriteLine("Added Device: " + d.FriendlyName);
            

            UPnP_SinkFunctions func = new UPnP_SinkFunctions(
                new SinkStack.CpAVTransport(d.GetServices(SinkStack.CpAVTransport.SERVICE_NAME)[0]),
                new SinkStack.CpConnectionManager(d.GetServices(SinkStack.CpConnectionManager.SERVICE_NAME)[0]),
                new SinkStack.CpRenderingControl(d.GetServices(SinkStack.CpRenderingControl.SERVICE_NAME)[0]));

            AddSinkEvent(func, null);
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
