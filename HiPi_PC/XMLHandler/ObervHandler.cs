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

        public delegate void musikUpdateDel(object s, trackEventArgs tracks);

        public event musikUpdateDel musikUpdateEvent;

        public delegate void playQueueUpdateDel(object s, trackEventArgs tracks);

        public event playQueueUpdateDel playQueueUpdateEvent;

        
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
           // _UPnPSink.GetVolume();
        }

        public void getUPnPSource(UPnP_SourceFunctions e, EventArgs s)
        {
            _UPnPSource = e;
            _UPnPSource.BrowseResult += getResult;
            _UPnPSource.Browse("all");
            _UPnPSource.Browse("playQueue");
            
        }

        public void getResult(object e, UPnP_SourceFunctions.UPnPEventArgs s)
        {
            Handle(s.Data);  
        } 

        public void Handle(string xml)
        {
            List<ITrack> list = xmlr.itemReader(xml);
            var args = new trackEventArgs(list);

            switch (list[0].ParentID)
            {
                case "playQueue":
                    playQueueUpdateEvent(this, args);
                    break;
                case "all":
                    musikUpdateEvent(this, args);
                    break;
                default:
                break;
            }
        }

        public void UpdateMusicindex(List<ITrack> listen)
        {
            
        }
        public void updateplayqueue(List<ITrack> listen)
        {
            /*
            playqueue.Clear();

            foreach (var track in listen)
            {
                playqueue.Add(track);
            }
            */
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

        public void GetVolume()
        {
            if (_UPnPSink != null)
                _UPnPSink.GetVolume();
        }
    }

    public class trackEventArgs : EventArgs
    {
        public List<ITrack> _tracks = new List<ITrack>();
 
        public trackEventArgs(List<ITrack> tracks)
        {
            _tracks = tracks;
        }
    }
}
