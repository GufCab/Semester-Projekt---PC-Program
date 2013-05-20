using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using Containers;

namespace XMLReader
{
    public class XMLReaderPC
    {
        public List<Container> containerReader(string xml)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load("container.xml");
            //xmlDocument.LoadXml(xml);

            var tmpList = new List<Container>();
            

            XmlNodeList nodeList = xmlDocument.GetElementsByTagName("container");

            foreach (XmlElement elm in nodeList)
            {
                var container = new Container();
                XmlNodeList titleList = elm.GetElementsByTagName("dc:title");
                container.title = titleList[0].InnerText;

                titleList = elm.GetElementsByTagName("dc:title");
                container.upnpClass = titleList[0].InnerText;

                container.id = elm.GetAttribute("id");
                container.parentID = elm.GetAttribute("parentID");
                
                tmpList.Add(container);
                
            }
            return tmpList;
        }

        public List<ITrack> itemReader(string xml)
        {
            var doc = new XmlDocument();
            doc.Load("item.xml");
            //xmlDocument.LoadXml(xml);

            var tracks = new List<ITrack>();

            XmlNodeList nodeList = doc.GetElementsByTagName("item");

            foreach (XmlElement elm in nodeList)
            {
                ITrack track = new Track();

                XmlNodeList titleList = elm.GetElementsByTagName("upnp:album");
                track.Album = titleList[0].InnerText;

                titleList = elm.GetElementsByTagName("dc:title");
                track.Title = titleList[0].InnerText;

                titleList = elm.GetElementsByTagName("upnp:artist");
                track.Artist = titleList[0].InnerText;

                titleList = elm.GetElementsByTagName("upnp:genre");
                track.Genre = titleList[0].InnerText;

                //ToDo string split path and ip
                titleList = elm.GetElementsByTagName("res");
                track.Path = titleList[0].InnerText;

                //ToDo string split path and ip
                track.DeviceIP = titleList[0].InnerText;

                track.Duration = titleList[0].Attributes["duration"].Value;
                track.Duration = titleList[0].Attributes["protocolInfo"].Value;

                tracks.Add(track);
            }
            return tracks;
        }
    }
}


