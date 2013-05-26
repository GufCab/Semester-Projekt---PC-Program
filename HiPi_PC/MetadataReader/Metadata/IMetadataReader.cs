using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataReader.Metadata
{
    /// <summary>
    /// This interface ensures the user has access to a certain range of metadata, about the called object.
    /// </summary>
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
