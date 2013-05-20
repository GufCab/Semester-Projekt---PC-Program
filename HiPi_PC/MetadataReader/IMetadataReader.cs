using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaReader.MetadataReader
{
    public interface IMetadataReader
    {
        string ItemName { get; }
        string Title { get; }
        string Artist { get; }
        string Album { get; }
        string Nr { get; }
        string Genre { get; }
        int LengthS { get; }
        string Lengthstring { get; }
        string Filepath { get; }

    }
}
