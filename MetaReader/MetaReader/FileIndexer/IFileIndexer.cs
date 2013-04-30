using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaReader.MetadataReader;

namespace MetaReader.FileIndexer
{
    interface IFileIndexer
    {
        List<string> FolderItemList { get; }
        void SetIndexPath(string folderpath);
        List<IMetadataReader> GetMetaData();
    }
}
