using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenSource.UPnP;
using SourceStack;

namespace UPnP_CP
{
    public interface ISourceFunctions
    {
        void Browse(string id);
    }

    public class UPnP_SourceFunctions : ISourceFunctions
    {
        private SourceStack.CpConnectionManager _ConnectionManager;
        private SourceStack.CpContentDirectory _ContentDirectory;
        public string _result;

        public delegate void ResultDelegate(object sender, SourceEventArgs e);
        
        public event ResultDelegate BrowseResult;

        
        
        public UPnP_SourceFunctions(CpConnectionManager CM, CpContentDirectory CD)
        {
            _ConnectionManager = CM;
            _ContentDirectory = CD;

            _ContentDirectory.OnResult_Browse += ContentDirectoryOnOnResultBrowse;
        }

        public void Browse(string s)
        {
            _ContentDirectory.Browse(s, CpContentDirectory.Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN, "*", 0, 0, "");
        }

        private void ContentDirectoryOnOnResultBrowse(CpContentDirectory sender, string objectId, CpContentDirectory.Enum_A_ARG_TYPE_BrowseFlag browseFlag, string filter, uint startingIndex, uint requestedCount, string sortCriteria, string result, uint numberReturned, uint totalMatches, uint updateId, UPnPInvokeException upnPInvokeException, object tag)
        {
            SourceEventArgs args = new SourceEventArgs(result);
            
            BrowseResult(this, args);
        }

        public class SourceEventArgs : EventArgs
        {
            public string _data { get; private set; }

            public SourceEventArgs(string data)
            {
                _data = data;
            }
        }

    }
}

