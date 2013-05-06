using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaReader.MetadataReader;

namespace MetadataReader
{
    public class Metadata : IMetadataReader 
    {
        public string ItemName { get;  set; }
        public string Title { get; set; }
        public string Artist { get;set; }
        public string Album { get; set; }
        public string Nr { get; set; }
        public string Genre { get; set; }
        public int LengthS { get; set; }
        public string Filepath { get; set; }

    }
}
