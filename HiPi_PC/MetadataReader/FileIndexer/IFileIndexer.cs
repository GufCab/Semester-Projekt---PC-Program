using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetadataReader.Metadata;

namespace MetadataReader.FileIndexer
{
    /// <summary>
    /// The interface IFileIndexer gives the user an easy way to collect data about files in a folder
    /// </summary>
    public interface IFileIndexer
    {
        List<string> FolderItemList { get; }
        void SetIndexPath(string folderpath);
        List<IMetadataReader> GetMetaData();
    }
}
