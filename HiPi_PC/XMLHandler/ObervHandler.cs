﻿using System;
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
       public ObservableCollection<ITrack> musikindex = new ObservableCollection<ITrack>();
       public ObservableCollection<ITrack> playqueue = new ObservableCollection<ITrack>();

        private UPnP_SinkFunctions _UPnPSink = null;
        private UPnP_SourceFunctions _UPnPSource = null;
        private UPnP_Setup setup = null;

        public ObervHandler()
        {
           // subscribe();
           // setup.StartSinkDisco();
            //setup.StartSourceDisco();
        }

        public void subscribe()
        {
            setup.AddSinkEvent += getUPnPSink;
            setup.AddSourceEvent += getUPnPSource;
            _UPnPSource.BrowseResult += getResult;
        }

        public void getUPnPSink(UPnP_SinkFunctions e, EventArgs s)
        {
            _UPnPSink = e;
        }

        public void getUPnPSource(UPnP_SourceFunctions e, EventArgs s)
        {
            _UPnPSource = e;

            _UPnPSource.Browse("All");
        }

        public void getResult(object e, EventArgs s)
        {
            Handle((string) e);
        }

        public void Handle(string xml)
        {
            var list = xmlr.itemReader(xml);

            switch (list[0].ParentID)
            {
                case "plauQueue":
                updateplayqeue(list);
                    break;
                case "All":
                    UpdateMusicindex(list);
                    break;
                default:
                break;

                    

            }
            list.Clear();

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

        public void UpdateMusicindex(List<ITrack> listen)
        {
            musikindex.Clear();


            foreach (var track in listen)
            {
                musikindex.Add(track);
            }
        }
        public void updateplayqeue(List<ITrack> listen)
        {
            playqueue.Clear();

            foreach (var track in listen)
            {
                playqueue.Add(track);
            }

        }
    }
}
