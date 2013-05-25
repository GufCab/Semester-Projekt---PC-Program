using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataReader.FileIndexer
{
    class ExtentionVerifier
    {
        //list of extentions to get metadata from
        public List<string> ExtentionList { get; private set; }

        public ExtentionVerifier()
        {
            ExtentionList = new List<string>();
            LoadExtentionList();
        }

        private void LoadExtentionList()
        {
            AddExtension(".wma");
            AddExtension(".mp3");
            AddExtension(".mp4");
            AddExtension(".wav");
            AddExtension(".flac");
            AddExtension(".ra");
        }

        public void AddExtension(string dotExtention)
        {
            if (ExtentionList.IndexOf(dotExtention) == -1)
            {
                ExtentionList.Add(dotExtention);
                //Console.WriteLine("Loaded extension {0}", dotExtention);
            }

            else
            {
                Console.WriteLine("Extension already exist for {0}", dotExtention);
            }
        }

        public bool TestExtention(string dotExtention)
        {
            if (ExtentionList.IndexOf(dotExtention) != -1)
            {
                return true;
            }
            return false;
        }
    }
}
