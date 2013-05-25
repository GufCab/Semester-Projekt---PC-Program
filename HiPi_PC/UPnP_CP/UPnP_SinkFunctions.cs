using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using OpenSource.UPnP;
using SinkStack;
using Containers;

namespace UPnP_CP
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISinkFunctions
    {
        //UPnP functions
        void Play();
        void Pause();
        void Stop();
        void Next();
        void Previous();
        void SetTransportURI(string path, string metaData);
        void SetNextTransportURI(string path, string metaData);
        void GetVolume();
        void SetVolume(ushort desiredVolume);
        void GetPosition();
        void SetPosition(ushort pos);
        void GetIpAddress();

        //Events
        event UPnP_SinkFunctions.getVolumeDel getVolEvent;
        event UPnP_SinkFunctions.getPositionDel getPositionEvent;
        event UPnP_SinkFunctions.getIPDel getIPEvent;
        event UPnP_SinkFunctions.transportStateDel transportStateEvent;
    }

    public class UPnP_SinkFunctions : ISinkFunctions
    {
        private SinkStack.CpAVTransport _AVTransport;
        private SinkStack.CpConnectionManager _ConnectionManager;
        private SinkStack.CpRenderingControl _RenderingControl;

        public delegate void getVolumeDel(object sender, EventArgsContainer<ushort> e);
        public event getVolumeDel getVolEvent;

        public delegate void getPositionDel(object sender, EventArgsContainer<List<ushort>>  e);
        public event getPositionDel getPositionEvent;

        public delegate void getIPDel(object sender, EventArgsContainer<string> e);
        public event getIPDel getIPEvent;

        public delegate void transportStateDel(object sender, EventArgsContainer<string> e);
        public event transportStateDel transportStateEvent;
        
        public uint InstanceID { get; private set; }
        private string _channel;
        private string _speed;
        private State value;

        private enum State
        {
            Playing,
            Stopped,
            Transitioning
        };

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
            
            _AVTransport.OnStateVariable_TransportState += AvTransportOnOnStateVariableTransportState;
            _AVTransport._subscribe(30);

            Timer subscribeTimer = new Timer();
            subscribeTimer.Elapsed += new ElapsedEventHandler(timerEventFunc);
            subscribeTimer.Interval = 30000;
            subscribeTimer.Enabled = true;
        }

        private void timerEventFunc(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _AVTransport._subscribe(30);
        }
        
        private void AvTransportOnOnStateVariableTransportState(CpAVTransport sender, string newValue)
        {
            switch (newValue)
            {
                case "PLAYING":
                    if (value == State.Stopped)
                    {
                        transportStateEvent(this, new EventArgsContainer<string>("playing"));
                    }
                    else if(value == State.Transitioning)
                    {
                        //get current number
                    }
                    value = State.Playing; 
                    break;

                case "STOPPED":
                    if (value == State.Playing)
                    {
                        transportStateEvent(this, new EventArgsContainer<string>("stopped"));
                    }
                    else if(value == State.Transitioning)
                    {
                        //?
                    }
                    value = State.Stopped; 
                    break;

                case "TRANSITIONING":
                    if (value == State.Playing)
                    {
                        //ready for new number
                    }
                    else if (value == State.Stopped)
                    {
                        //?
                    }
                    value = State.Transitioning; 
                    break;
            }
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
            EventArgsContainer<ushort> argsContainer = new EventArgsContainer<ushort>(currentVolume);

            getVolEvent(this, argsContainer);
        }

        public void SetTransportURI(string path, string metaData)
        {
            _AVTransport.SetAVTransportURI(0, path, metaData);
        }

        public void SetNextTransportURI(string path, string metaData)
        {
            _AVTransport.SetNextAVTransportURI(0, metaData, "");
        }

        public void GetPosition()
        {
            _RenderingControl.GetPosition(0);
        }

        private void RenderingControlOnOnResultGetPosition(CpRenderingControl sender, uint instanceId, ushort currentPosition, ushort duration, UPnPInvokeException upnPInvokeException, object tag)
        {
            var list = new List<ushort>{currentPosition, duration};

            var args = new EventArgsContainer<List<ushort>>(list);

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
            EventArgsContainer<string> argsContainer = new EventArgsContainer<string>(ipAddress);

            getIPEvent(this, argsContainer);
        }
    }
}
