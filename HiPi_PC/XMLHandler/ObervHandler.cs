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
        //public ObservableCollection<ITrack> musikindex = new ObservableCollection<ITrack>();
        //public ObservableCollection<ITrack> playqueue = new ObservableCollection<ITrack>();

        private UPnP_SinkFunctions _UPnPSink = null;
        private UPnP_SourceFunctions _UPnPSource = null;
        private UPnP_Setup setup = null;

        private XMLWriter xmlWriter;

        public delegate void musikUpdateDel(object s, MyEventArgs<List<ITrack>> tracks);
        public event musikUpdateDel musikUpdateEvent;

        public delegate void playQueueUpdateDel(object s, MyEventArgs<List<ITrack>> tracks);
        public event playQueueUpdateDel playQueueUpdateEvent;

        public delegate void volumeDel(object s, MyEventArgs<ushort> vol);
        public event volumeDel VolumeUpdateEvent;

        public delegate void getPositionDel(object sender, MyEventArgs<List<ushort>> e);
        public event getPositionDel getPositionEvent;

        public delegate void getIPDel(object sender, MyEventArgs<string> e);
        public event getIPDel getIPEvent1;

        
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

        public void getResult(object e, UPnP_SourceFunctions.SourceEventArgs s)
        {
            Handle(s._data);  
        }

        private void getVolEvent(object sender, SinkEventArgs<ushort> volEventArgs)
        {
            var args = new MyEventArgs<ushort>(volEventArgs._data);

            VolumeUpdateEvent(this, args);
        }

        private void getPosEvent(object sender, SinkEventArgs<List<ushort>> posEventArgs)
        {
            //var args = new MyEventArgs<ushort>(posEventArgs._data);

            //getPositionEvent(this, args);

            var list = new List<ushort> { posEventArgs._data[0], posEventArgs._data[1] };

            MyEventArgs<List<ushort>> args = new MyEventArgs<List<ushort>>(list);

            getPositionEvent(this, args);
        }

        private void getIPEvent(object sender, SinkEventArgs<string> ipEventArgs)
        {
            var args = new MyEventArgs<string>(ipEventArgs._data);

            getIPEvent1(this, args);
        }

        public void getIP()
        {
            _UPnPSink.getIPEvent += getIPEvent;
        }

        public void Handle(string xml)
        {
            List<ITrack> list = xmlr.itemReader(xml);
            var args = new MyEventArgs<List<ITrack>>(list);

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

    public class MyEventArgs<T> : EventArgs
    {
        public T _data { get; private set; }

        public MyEventArgs(T data)
        {
            _data = data;
        }
    }
}
