﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenSource.DeviceBuilder;
using OpenSource.UPnP;

/// <summary>
/// Wrapper classes for UPnP functions and discovery
/// </summary>
namespace UPnP_CP
{
    /// <summary>
    /// Detects UPnP devices and delegates the necessary information to the appropriate objects
    /// </summary>
    public class UPnP_Setup
    {
        private static MediaRendererDiscovery _SinkDisco;
        private static MediaServerDiscovery _SourceDisco;

        //private UPnP_SinkFunctions Sink;

        public delegate void AddSinkHandler(ISinkFunctions e, EventArgs s);
        public event AddSinkHandler AddSinkEvent;

        public delegate void AddSourceHandler(ISourceFunctions e, EventArgs s);
        public event AddSourceHandler AddSourceEvent;

        public delegate void RemoveSourceHandler(object e, EventArgs s);
        public event RemoveSourceHandler RemoveSourceEvent;

        /// <summary>
        /// Start looking for source and sink
        /// </summary>
        public void StartServices()
        {
            StartSinkDisco();
            StartSourceDisco();
        }

        /// <summary>
        /// Creates a MediaServerDiscovery, subscribes to add and remove events and runs the start command
        /// </summary>
        private void StartSourceDisco()
        {
            _SourceDisco = new MediaServerDiscovery();
            _SourceDisco.OnAddedDevice += new MediaServerDiscovery.DiscoveryHandler(AddSource);
            _SourceDisco.OnRemovedDevice += new MediaServerDiscovery.DiscoveryHandler(RemoveSource);
            _SourceDisco.Start();
        }

        /// <summary>
        /// Creates a MediaRendererDiscovery, subscribes to add- and remove-events and then runs the start command
        /// </summary>
        private void StartSinkDisco()
        {
            _SinkDisco = new MediaRendererDiscovery();
            _SinkDisco.OnAddedDevice += new MediaRendererDiscovery.DiscoveryHandler(AddSink);
            _SinkDisco.OnRemovedDevice += new MediaRendererDiscovery.DiscoveryHandler(RemoveSink);
            _SinkDisco.Start();
        }
        
        /// <summary>
        /// Eventfunction that is run when an UPnP sink is detected on the network. Only accepts sinks with the friendly name: "HiPi - Sink". 
        /// If it has the right name then it creates three stacks (AVTransport, ConnectionManager and RenderingControl)
        /// </summary>
        /// <param name="sender">The object that send the event</param>
        /// <param name="d">The discovered sink device</param>
        private void AddSink(MediaRendererDiscovery sender, UPnPDevice d)
        {
            Console.WriteLine("Discovered Sink Device: " + d.FriendlyName);
            if (d.FriendlyName == "HiPi - Sink")
            {
                Console.WriteLine("Added Sink Device: " + d.FriendlyName);

                ISinkFunctions func = new UPnP_SinkFunctions(
                    new SinkStack.CpAVTransport(d.GetServices(SinkStack.CpAVTransport.SERVICE_NAME)[0]), 
                    new SinkStack.CpConnectionManager(d.GetServices(SinkStack.CpConnectionManager.SERVICE_NAME)[0]),
                    new SinkStack.CpRenderingControl(d.GetServices(SinkStack.CpRenderingControl.SERVICE_NAME)[0]));
                
                AddSinkEvent(func, null);
            }
        }

        /// <summary>
        /// NOT IMPLEMENTED. Should remove the sink from the controlpoint. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="d"></param>
        private static void RemoveSink(MediaRendererDiscovery sender, UPnPDevice d)
        {
            Console.WriteLine("Removed Device: " + d.FriendlyName);
            //Todo: Remove device somehow
        }

        /// <summary>
        /// Eventfunction that is run when an UPnP source is detected on the network. Only accepts sources with the friendly name: "HiPi - Source". 
        /// If it has the right name then it creates one stacks (ContentDirectory)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="d"></param>
        private void AddSource(MediaServerDiscovery sender, UPnPDevice d)
        {
            Console.WriteLine("Discovered Source Device: " + d.FriendlyName);

            if (d.FriendlyName == "HiPi - Source")
            {
                Console.WriteLine("Added Source Device: " + d.FriendlyName);

                ISourceFunctions func = new UPnP_SourceFunctions(null,
                    //new SourceStack.CpConnectionManager(d.GetServices(SourceStack.CpConnectionManager.SERVICE_NAME)[0]),
                    new SourceStack.CpContentDirectory(d.GetServices(SourceStack.CpContentDirectory.SERVICE_NAME)[0]));

                AddSourceEvent(func, null);
            }
        }

        /// <summary>
        /// NOT IMPLEMENTED. Should remove the source from the controlpoint. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="d"></param>
        private void RemoveSource(MediaServerDiscovery sender, UPnPDevice d)
        {
            Console.WriteLine("Device removed");
            RemoveSourceEvent(null, null);
        }
    }
}
