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

        public delegate void getVolumeDel(object sender, UPnPEventArgs e);
        public event getVolumeDel getVolEvent;

        public uint InstanceID { get; private set; }

        public UPnP_SinkFunctions(CpAVTransport av, CpConnectionManager cm, CpRenderingControl rc)
        {
            InstanceID = 0;
            _AVTransport = av;
            _ConnectionManager = cm;
            _RenderingControl = rc;

            _RenderingControl.OnResult_GetVolume += RenderingControlOnOnResultGetVolume;
        }

        public void Play()
        {
            _AVTransport.Play(InstanceID, CpAVTransport.Enum_TransportPlaySpeed._1);   
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
            _RenderingControl.SetVolume(0, CpRenderingControl.Enum_A_ARG_TYPE_Channel.MASTER, desiredVolume);
        }

        public void GetVolume()
        {
            _RenderingControl.GetVolume(0, CpRenderingControl.Enum_A_ARG_TYPE_Channel.MASTER);
        }

        private void RenderingControlOnOnResultGetVolume(CpRenderingControl sender, uint instanceId, CpRenderingControl.Enum_A_ARG_TYPE_Channel channel, ushort currentVolume, UPnPInvokeException upnPInvokeException, object tag)
        {
            UPnPEventArgs args = new UPnPEventArgs(currentVolume);

            getVolEvent(this, args);
        }

        public void SetTransportURI(string path, string metaData)
        {
            _AVTransport.SetAVTransportURI(0, path, metaData);
        }

        public void SetNextTransportURI(string path, string metaData)
        {
            //_AVTransport.SetNextAVTransportURI(0, path, metaData);
        }

    }

    public class UPnPEventArgs : EventArgs
    {
        public ushort Data { get; private set; }

        public UPnPEventArgs(ushort data)
        {
            Data = data;
        }
    }
}
