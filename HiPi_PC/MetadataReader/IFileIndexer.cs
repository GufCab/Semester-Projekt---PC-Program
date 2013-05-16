using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaReader.MetadataReader;

namespace MetaReader.FileIndexer
{
   public interface IFileIndexer
    {
        List<string> FolderItemList { get; }
        bool SetIndexPath(string folderpath);
        List<IMetadataReader> GetMetaData();
    }
}
