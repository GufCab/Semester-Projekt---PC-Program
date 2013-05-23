using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSource.UPnP;
using SinkStack;

namespace UPnP_CP
{
    public interface ISinkFunctions
    {
        void Play();
        void Pause();
        void Stop();
        void Next();
        void Previous();
        void SetTransportURI(string path, string metaData);
        void SetVolume(ushort desiredVolume);
    }

    public class UPnP_SinkFunctions : ISinkFunctions
    {
        private SinkStack.CpAVTransport _AVTransport;
        private SinkStack.CpConnectionManager _ConnectionManager;
        private SinkStack.CpRenderingControl _RenderingControl;

        public delegate void getVolumeDel(object sender, SinkEventArgs<ushort> e);
        public event getVolumeDel getVolEvent;

        public delegate void getPositionDel(object sender, SinkEventArgs<List<ushort>>  e);
        public event getPositionDel getPositionEvent;

        public delegate void getIPDel(object sender, SinkEventArgs<string> e);
        public event getIPDel getIPEvent;

        public uint InstanceID { get; private set; }
        private string _channel;
        private string _speed;

        public UPnP_SinkFunctions(CpAVTransport av, CpConnectionManager cm, CpRenderingControl rc)
        {
            InstanceID = 0;
            _AVTransport = av;
            _ConnectionManager = cm;
            _RenderingControl = rc;
            _channel = "1";
            _speed = "1";

            _RenderingControl.OnResult_GetVolume += RenderingControlOnOnResultGetVolume;
            _ConnectionManager.OnResult_GetIPAddress += ConnectionManagerOnOnResultGetIpAddress;
            _RenderingControl.OnResult_GetPosition += RenderingControlOnOnResultGetPosition;
        }
        
        public void Play()
        {
            _AVTransport.Play(InstanceID, _speed);   
        }

        public void Pause()
        {
            _AVTransport.Pause(InstanceID);
        }

        public void Stop()
        {
            _AVTransport.Stop(InstanceID);
        }

        public void Next()
        {
            _AVTransport.Next(InstanceID);
        }

        public void Previous()
        {
            _AVTransport.Previous(0);
        }

        public void SetVolume(ushort desiredVolume)
        {
            _RenderingControl.SetVolume(0, _channel, desiredVolume);
        }

        public void GetVolume()
        {
            _RenderingControl.GetVolume(0, _channel);
        }

        private void RenderingControlOnOnResultGetVolume(CpRenderingControl sender, uint instanceId, string channel, ushort currentVolume, UPnPInvokeException upnPInvokeException, object tag)
        {
            SinkEventArgs<ushort> args = new SinkEventArgs<ushort>(currentVolume);

            getVolEvent(this, args);
        }

        public void SetTransportURI(string path, string metaData)
        {
            _AVTransport.SetAVTransportURI(0, path, metaData);
        }

        public void SetNextTransportURI(string path, string metaData)
        {
            _AVTransport.SetNextAVTransportURI(0, path, metaData);
        }

        public void GetPosition()
        {
            _RenderingControl.GetPosition(0);
        }

        private void RenderingControlOnOnResultGetPosition(CpRenderingControl sender, uint instanceId, ushort currentPosition, ushort duration, UPnPInvokeException upnPInvokeException, object tag)
        {
            var list = new List<ushort>{currentPosition, duration};

            SinkEventArgs<List<ushort>> args = new SinkEventArgs<List<ushort>>(list);

            getPositionEvent(this, args);
        }
        
        public void SetPosition(ushort pos)
        {
            _RenderingControl.SetPosition(0, pos);
        }

        public void GetIpAddress()
        {
            _ConnectionManager.GetIPAddress();
        }

        private void ConnectionManagerOnOnResultGetIpAddress(CpConnectionManager sender, string ipAddress, UPnPInvokeException upnPInvokeException, object tag)
        {
            SinkEventArgs<string> args = new SinkEventArgs<string>(ipAddress);

            getIPEvent(this, args);
        }
    }

    public class SinkEventArgs<T> : EventArgs
    {
        public T _data { get; private set; }

        public SinkEventArgs(T data)
        {
            _data = data;
        }
    }
}
