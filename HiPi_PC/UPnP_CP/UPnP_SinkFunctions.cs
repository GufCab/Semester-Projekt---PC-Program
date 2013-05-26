using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using OpenSource.UPnP;
using SinkStack;
using Containers;
using XMLHandler;

namespace UPnP_CP
{
    /// <summary>
    /// Interface for UPnP functions and events
    /// </summary>
    public interface ISinkFunctions
    {
        //UPnP functions
        void Play();
        void Pause();
        void Stop();
        void Next();
        void Previous();
        void SetTransportURI(ITrack track);
        void SetNextTransportURI(ITrack track);
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

    /// <summary>
    /// Handles the connection to the Intel generated UPnP sink functions
    /// </summary>
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

        private XMLReader _xmlReader = new XMLReader();
        private XMLWriter _xmlWriter = new XMLWriter();

        private const uint _instanceID = 0;
        private const string _channel = "1";
        private const string _speed = "1";
        private State value;

        Timer subscribeTimer = new Timer();

        private enum State
        {
            Playing,
            Stopped,
            Transitioning
        };

        /// <summary>
        /// Sets the connection to the sink stacks and subscribs to events
        /// </summary>
        /// <param name="av">AVTransport stack</param>
        /// <param name="cm">ConnectionManager stack</param>
        /// <param name="rc">RenderingControl stack</param>
        public UPnP_SinkFunctions(CpAVTransport av, CpConnectionManager cm, CpRenderingControl rc)
        {
            _AVTransport = av;
            _ConnectionManager = cm;
            _RenderingControl = rc;

            _RenderingControl.OnResult_GetVolume += RenderingControlOnOnResultGetVolume;
            _ConnectionManager.OnResult_GetIPAddress += ConnectionManagerOnOnResultGetIpAddress;
            _RenderingControl.OnResult_GetPosition += RenderingControlOnOnResultGetPosition;
            
            _AVTransport.OnStateVariable_TransportState += AvTransportOnOnStateVariableTransportState;
            _AVTransport._subscribe(30);
            
            subscribeTimer.Elapsed += new ElapsedEventHandler(timerEventFunc);
            subscribeTimer.Interval = 30000;
            subscribeTimer.Enabled = true;

            Startup();
        }

        /// <summary>
        /// Runs the UPnP functions needed at startup
        /// </summary>
        private void Startup()
        {
            GetPosition();
            GetVolume();
            //GetIpAddress();
        }

        /// <summary>
        /// Event that is fired every 30 second so the controlpoint can resubscribe to events from the upnp device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="elapsedEventArgs"></param>
        private void timerEventFunc(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _AVTransport._subscribe(30);
        }
        
        /// <summary>
        /// Event that is fired when a transportstate is changed on the upnp device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newValue">New state</param>
        private void AvTransportOnOnStateVariableTransportState(CpAVTransport sender, string newValue)
        {
            switch (newValue)
            {
                case "PLAYING":
                    if (value == State.Stopped)
                    {
                        transportStateEvent(this, new EventArgsContainer<string>(newValue));
                    }
                    else if(value == State.Transitioning)
                    {
                        transportStateEvent(this, new EventArgsContainer<string>("BROWSE"));
                    }
                    value = State.Playing; 
                    break;

                case "STOPPED":
                    if (value == State.Playing)
                    {
                        transportStateEvent(this, new EventArgsContainer<string>(newValue));
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
            if (_AVTransport != null)
            {
                _AVTransport.Play(_instanceID, _speed);   
            }
        }

        public void Pause()
        {
            if (_AVTransport != null)
            {
                _AVTransport.Pause(_instanceID);
            }
        }

        public void Stop()
        {
            if (_AVTransport != null)
            {
                _AVTransport.Stop(_instanceID);
            }
        }

        public void Next()
        {
            if (_AVTransport != null)
            {
                _AVTransport.Next(_instanceID);
            }
        }

        public void Previous()
        {
            if (_AVTransport != null)
            {
                _AVTransport.Previous(0);
            }
        }

        public void SetVolume(ushort desiredVolume)
        {
            if (_RenderingControl != null)
            {
                _RenderingControl.SetVolume(0, _channel, desiredVolume);
            }
        }

        public void GetVolume()
        {
            if (_RenderingControl != null)
            {
                _RenderingControl.GetVolume(0, _channel);
            }
        }

        private void RenderingControlOnOnResultGetVolume(CpRenderingControl sender, uint instanceId, string channel, ushort currentVolume, UPnPInvokeException upnPInvokeException, object tag)
        {
            EventArgsContainer<ushort> argsContainer = new EventArgsContainer<ushort>(currentVolume);

            getVolEvent(this, argsContainer);
        }

        public void SetTransportURI(ITrack track)
        {
            if (_AVTransport != null)
            {
                string Path = track.Protocol + track.DeviceIP + track.Path + track.FileName;
                string metaData = _xmlWriter.ConvertITrackToXML(new List<ITrack> { track });

                _AVTransport.SetAVTransportURI(0, Path, metaData);
            }
        }

        public void SetNextTransportURI(ITrack track)
        {
            if (_AVTransport != null)
            {
                string Path = track.Protocol + track.DeviceIP + track.Path + track.FileName;
                string metaData = _xmlWriter.ConvertITrackToXML(new List<ITrack> { track });

                _AVTransport.SetNextAVTransportURI(0, metaData, "");
            }
        }

        public void GetPosition()
        {
            if (_RenderingControl != null)
            {
                _RenderingControl.GetPosition(0);
            }
        }

        private void RenderingControlOnOnResultGetPosition(CpRenderingControl sender, uint instanceId, ushort currentPosition, ushort duration, UPnPInvokeException upnPInvokeException, object tag)
        {
            var list = new List<ushort>{currentPosition, duration};

            var args = new EventArgsContainer<List<ushort>>(list);

            getPositionEvent(this, args);
        }
        
        public void SetPosition(ushort pos)
        {
            if (_RenderingControl != null)
            {
                _RenderingControl.SetPosition(0, pos);    
            }
        }

        public void GetIpAddress()
        {
            if (_ConnectionManager != null)
            {
                _ConnectionManager.GetIPAddress();   
            }
        }

        private void ConnectionManagerOnOnResultGetIpAddress(CpConnectionManager sender, string ipAddress, UPnPInvokeException upnPInvokeException, object tag)
        {
            EventArgsContainer<string> argsContainer = new EventArgsContainer<string>(ipAddress);

            getIPEvent(this, argsContainer);
        }
    }
}
