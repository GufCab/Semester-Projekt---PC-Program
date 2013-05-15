using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public uint InstanceID { get; private set; }

        public UPnP_SinkFunctions(CpAVTransport av, CpConnectionManager cm, CpRenderingControl rc)
        {
            InstanceID = 0;
            _AVTransport = av;
            _ConnectionManager = cm;
            _RenderingControl = rc;
        }
        
        /// <summary>
        /// Invoke play on device. Always uses PlaySpeed '1'
        /// </summary>
        /// <param name="instanceID"></param>
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

        public void SetTransportURI(string path, string metaData)
        {
            _AVTransport.SetAVTransportURI(0, path, metaData);
        }

    }
}
