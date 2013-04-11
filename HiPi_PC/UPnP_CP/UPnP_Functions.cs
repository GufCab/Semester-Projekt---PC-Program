using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SinkStack;

namespace UPnP_CP
{
    public class UPnP_Functions
    {
        //public UPnP_Setup

        public static SinkStack.CpAVTransport _AVTransport;
        public static SinkStack.CpConnectionManager _ConnectionManager;
        public static SinkStack.CpRenderingControl _RenderingControl;

        public void Play()
        {
            _AVTransport.Play(0, CpAVTransport.Enum_TransportPlaySpeed._1);   
        }
    }
}
