using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateSync
{
    interface IMetadata
    {
        string Title { get; }
        string Kunstner { get; }
        string Album { get; }
        string Genre { get; }
        int Nrlenth { get; }
        string FilePath { get; }

    }
}
