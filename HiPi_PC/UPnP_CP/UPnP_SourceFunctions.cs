using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSource.UPnP;
using SourceStack;
using Containers;
using XMLHandler;

namespace UPnP_CP
{
    /// <summary>
    /// Interface for UPnP functions and events
    /// </summary>
    public interface ISourceFunctions
    {
        //UPnP functions
        void Browse(string id);

        //Events
        event UPnP_SourceFunctions.ResultDelegate BrowseResult;
    }

    /// <summary>
    /// Handles the connection to the Intel generated UPnP source functions
    /// </summary>
    public class UPnP_SourceFunctions : ISourceFunctions
    {
        private SourceStack.CpConnectionManager _ConnectionManager;
        private SourceStack.CpContentDirectory _ContentDirectory;
        public string _result;

        private XMLReader _xmlReader = new XMLReader();

        public delegate void ResultDelegate(object sender, List<ITrack> e);
        public event ResultDelegate BrowseResult;

        
        /// <summary>
        /// Sets the connection to the source stacks and subscribs to events
        /// </summary>
        /// <param name="CM">ConnectionManager stack</param>
        /// <param name="CD">ContentDirectory stack</param>
        public UPnP_SourceFunctions(CpConnectionManager CM, CpContentDirectory CD)
        {
            _ConnectionManager = CM;
            _ContentDirectory = CD;

            _ContentDirectory.OnResult_Browse += ContentDirectoryOnOnResultBrowse;
        }

        public void Startup()
        {
            Browse("all");
            Browse("playqueue");
        }

        /// <summary>
        /// Function to make the UPnP browse command more simple
        /// </summary>
        /// <param name="objectId">What you want to browse</param>
        public void Browse(string objectId)
        {
            _ContentDirectory.Browse(objectId, CpContentDirectory.Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN, "*", 0, 0, "");
        }

        /// <summary>
        /// Event that is raised when the upnp device answers after a browse command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="objectId"></param>
        /// <param name="browseFlag"></param>
        /// <param name="filter"></param>
        /// <param name="startingIndex"></param>
        /// <param name="requestedCount"></param>
        /// <param name="sortCriteria"></param>
        /// <param name="result">string containing the returned xml</param>
        /// <param name="numberReturned"></param>
        /// <param name="totalMatches"></param>
        /// <param name="updateId"></param>
        /// <param name="upnPInvokeException"></param>
        /// <param name="tag"></param>
        private void ContentDirectoryOnOnResultBrowse(CpContentDirectory sender, string objectId, CpContentDirectory.Enum_A_ARG_TYPE_BrowseFlag browseFlag, string filter, uint startingIndex, uint requestedCount, string sortCriteria, string result, uint numberReturned, uint totalMatches, uint updateId, UPnPInvokeException upnPInvokeException, object tag)
        {
            List<ITrack> tracks = _xmlReader.itemReader(result);
            
            BrowseResult(this, tracks);
        }
    }
}

