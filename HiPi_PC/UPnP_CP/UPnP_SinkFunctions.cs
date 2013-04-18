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
        //void SetTransportURI();
        void SetVolume(ushort desiredVolume);
    }

    public class UPnP_SinkFunctions : ISinkFunctions
    {
        private SinkStack.CpAVTransport _AVTransport;
        private SinkStack.CpConnectionManager _ConnectionManager;
        private SinkStack.CpRenderingControl _RenderingControl;

        public UPnP_SinkFunctions(CpAVTransport AV, CpConnectionManager CM, CpRenderingControl RC)
        {
            _AVTransport = AV;
            _ConnectionManager = CM;
            _RenderingControl = RC;
        }
        
        public void Play()
        {
            _AVTransport.Play(0, CpAVTransport.Enum_TransportPlaySpeed._1);   
        }

        public void Pause()
        {
            _AVTransport.Pause(0);
        }

        public void Stop()
        {
            _AVTransport.Stop(0);
        }

        public void Next()
        {
            _AVTransport.Next(0);
        }

        public void Previous()
        {
            _AVTransport.Previous(0);
        }

        public void SetTransportURI()
        {
            //_AVTransport
        }

        public void SetVolume(ushort desiredVolume)
        {
            _RenderingControl.SetVolume(0,CpRenderingControl.Enum_A_ARG_TYPE_Channel.MASTER, desiredVolume);
        }

        //public void 

    }
}
