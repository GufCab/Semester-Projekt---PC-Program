using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Containers;
using UPnP_CP;
using XMLReader;


namespace XMLHandler
{
    public class ObervHandler
    {
        XMLReaderPC xmlr = new XMLReaderPC();

        private ISinkFunctions _UPnPSink = null;
        private ISourceFunctions _UPnPSource = null;
        private UPnP_Setup setup = null;

        private XMLWriter xmlWriter;

        public delegate void musikUpdateDel(object s, EventArgsContainer<List<ITrack>> tracks);
        public event musikUpdateDel musikUpdateEvent;

        public delegate void playQueueUpdateDel(object s, EventArgsContainer<List<ITrack>> tracks);
        public event playQueueUpdateDel playQueueUpdateEvent;

        public delegate void volumeDel(object s, EventArgsContainer<ushort> vol);
        public event volumeDel VolumeUpdateEvent;

        public delegate void getPositionDel(object sender, EventArgsContainer<List<ushort>> e);
        public event getPositionDel getPositionEvent;

        public delegate void getIPDel(object sender, EventArgsContainer<string> e);
        public event getIPDel getIPEvent1;

        public delegate void transportStateDel(object sender, EventArgsContainer<string> e);
        public event transportStateDel transportStateEvent;

        
        public ObervHandler()
        {
            xmlWriter = new XMLWriter();
            setup = new UPnP_Setup();

            subscribe();
            setup.StartSinkDisco();
            setup.StartSourceDisco();
        }

        public void subscribe()
        {
            setup.AddSinkEvent += getUPnPSink;
            setup.AddSourceEvent += getUPnPSource;
        }

        public void getUPnPSink(UPnP_SinkFunctions e, EventArgs s)
        {
            _UPnPSink = e;
            _UPnPSink.getVolEvent += getVolEvent;
            _UPnPSink.getPositionEvent += getPosEvent;
            _UPnPSink.transportStateEvent += UpnPSinkOnTransportStateEvent;
            _UPnPSink.GetVolume();
            _UPnPSink.GetPosition();
        }
        
        public void getUPnPSource(UPnP_SourceFunctions e, EventArgs s)
        {
            _UPnPSource = e;
            _UPnPSource.BrowseResult += getResult;
            _UPnPSource.Browse("all");
            _UPnPSource.Browse("playqueue");
        }

        private void UpnPSinkOnTransportStateEvent(object sender, EventArgsContainer<string> eventArgsContainer)
        {
            this.transportStateEvent(this, eventArgsContainer);
        }

        public void getResult(object e, Containers.EventArgsContainer<string> s)
        {
            Handle(s._data);  
        }

        private void getVolEvent(object sender, Containers.EventArgsContainer<ushort> volEventArgsContainer)
        {
            var args = new EventArgsContainer<ushort>(volEventArgsContainer._data);

            VolumeUpdateEvent(this, args);
        }

        private void getPosEvent(object sender, Containers.EventArgsContainer<List<ushort>> posEventArgsContainer)
        {
            //var args = new MyEventArgs<ushort>(posEventArgs._data);

            //getPositionEvent(this, args);

            var list = new List<ushort> { posEventArgsContainer._data[0], posEventArgsContainer._data[1] };

            EventArgsContainer<List<ushort>> argsContainer = new EventArgsContainer<List<ushort>>(list);

            getPositionEvent(this, argsContainer);
        }

        private void getIPEvent(object sender, Containers.EventArgsContainer<string> ipEventArgsContainer)
        {
            var args = new EventArgsContainer<string>(ipEventArgsContainer._data);

            getIPEvent1(this, args);
        }

        public void getIP()
        {
            _UPnPSink.getIPEvent += getIPEvent;
        }

        public void Handle(string xml)
        {
            List<ITrack> list = xmlr.itemReader(xml);
            var args = new EventArgsContainer<List<ITrack>>(list);

            if (list.Count > 0)
            {
                switch (list[0].ParentID)
                {
                    case "playqueue":
                        playQueueUpdateEvent(this, args);
                        break;
                    case "all":
                        musikUpdateEvent(this, args);
                        //_UPnPSource.Browse("playqueue");
                        break;
                    default:
                        break;
                }
            }
        }

        public void Play()
        {
            if (_UPnPSink != null)
                _UPnPSink.Play();
        }

        public void Pause()
        {
            if (_UPnPSink != null)
                _UPnPSink.Pause();
        }

        public void Stop()
        {
            if (_UPnPSink != null)
                _UPnPSink.Stop();
        }

        public void Next()
        {
            if (_UPnPSink != null)
                _UPnPSink.Next();
        }

        public void Previous()
        {
            if (_UPnPSink != null)
                _UPnPSink.Previous();
        }

        public void SetAVTransportURI(ITrack track)
        {
            string Path = track.Protocol + track.DeviceIP + track.Path + track.FileName;
            string metaData = xmlWriter.ConvertITrackToXML(new List<ITrack>(){track});

            if(_UPnPSink != null)
                _UPnPSink.SetTransportURI(Path, metaData);
        }

        public void SetNextAVTransportURI(ITrack track)
        {
            string Path = track.Protocol + track.DeviceIP + track.Path + track.FileName;
            string metaData = xmlWriter.ConvertITrackToXML(new List<ITrack>() { track });

            if (_UPnPSink != null)
                _UPnPSink.SetNextTransportURI(Path, metaData);
        }

        public void GetVolume()
        {
            if (_UPnPSink != null)
                _UPnPSink.GetVolume();
        }

        public void SetVolume(uint vol)
        {
            if (_UPnPSink != null)
                _UPnPSink.SetVolume((ushort) vol);
        }

        public void GetPosition()
        {
            if (_UPnPSink != null)
                _UPnPSink.GetPosition();
        }

        public void SetPosition(ushort pos)
        {
            if (_UPnPSink != null)
                _UPnPSink.SetPosition(pos);
        }

        public void GetIPaddress()
        {
            if (_UPnPSink != null)
                _UPnPSink.GetIpAddress();
        }
    }
}
