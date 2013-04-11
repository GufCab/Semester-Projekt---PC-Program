// UPnP .NET Framework Control Point Stack, Discovery Module
// Device Builder Build#1.0.4144.25068

using System;
using System.Net;
using OpenSource.UPnP;
using OpenSource.Utilities;
using OpenSource.DeviceBuilder;

namespace OpenSource.DeviceBuilder
{
	/// <summary>
	/// Summary description for MediaRendererDiscovery.
	/// </summary>
	class MediaRendererDiscovery
	{
		private UPnPSmartControlPoint scp;
		private WeakEvent AddEvent = new WeakEvent();
		private WeakEvent RemoveEvent = new WeakEvent();
		
		public delegate void DiscoveryHandler(MediaRendererDiscovery sender, UPnPDevice dev);
		public event DiscoveryHandler OnAddedDevice
		{
			add
			{
				AddEvent.Register(value);
			}
			remove
			{
				AddEvent.UnRegister(value);
			}
		}
		
		public event DiscoveryHandler OnRemovedDevice
		{
			add
			{
				RemoveEvent.Register(value);
			}
			remove
			{
				RemoveEvent.UnRegister(value);
			}
		}
		
		public MediaRendererDiscovery()
		{
		}
		
		public void Start()
		{
			scp = new UPnPSmartControlPoint(new UPnPSmartControlPoint.DeviceHandler(OnAddSink), null ,"urn:schemas-upnp-org:device:MediaRenderer:1");
			scp.OnRemovedDevice += new UPnPSmartControlPoint.DeviceHandler(OnRemoveSink);
		}
		
		public void ReScan()
		{
			scp.Rescan();
		}
		
		public void UnicastSearch(IPAddress address)
		{
			scp.UnicastSearch(address);
		}
		
		public void ForceDisposeDevice(UPnPDevice dev)
		{
			if (dev.ParentDevice != null)
			{
				ForceDisposeDevice(dev.ParentDevice);
			}
			else
			{
				scp.ForceDisposeDevice(dev);
			}
		}
		
		private void OnAddSink(UPnPSmartControlPoint sender, UPnPDevice d)
		{
			AddEvent.Fire(this, d);
		}
		
		private void OnRemoveSink(UPnPSmartControlPoint sender, UPnPDevice d)
		{
			RemoveEvent.Fire(this, d);
		}
	}
}

