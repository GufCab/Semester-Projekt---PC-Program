using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MetadataReader.FileIndexer
{
    /// <summary>
    /// Classen ExtentionVerifier secures that the asked extention is 
    /// of a verified music type
    /// </summary>
    class ExtentionVerifier
    {
        //list of extentions to get metadata from
        public List<string> ExtentionList { get; private set; }

        /// <summary>
        /// Constructor secures that a ExtentionList is loaded, and is not null
        /// </summary>
        public ExtentionVerifier()
        {
            ExtentionList = new List<string>();
            LoadExtentionList();
        }

        /// <summary>
        /// Loads a number of music extentions to the list: ExtentionList
        /// (.wma, .mp3, .mp4, .wav, .flac and .ra)
        /// </summary>
        private void LoadExtentionList()
        {
            AddExtension(".wma");
            AddExtension(".mp3");
            AddExtension(".mp4");
            AddExtension(".wav");
            AddExtension(".flac");
            AddExtension(".ra");
        }

        /// <summary>
        /// Makes it posible to add extentions to the list, so more extentions is posible to
        /// </summary>
        /// <param name="dotExtention">The extention that are to be added to the list off extentions. </param>
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

        /// <summary>
        /// Test the dotExtention against the extentionlist
        /// </summary>
        /// <param name="dotExtention">The extention that are to be testet</param>
        /// <returns>returns true if it exists</returns>
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
