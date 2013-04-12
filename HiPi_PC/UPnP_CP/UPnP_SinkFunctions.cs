using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SinkStack;

namespace UPnP_CP
{
    public class UPnP_SinkFunctions
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
    }
}
