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

        public delegate void ResultDelegate(object sender, EventArgs e);

        public event ResultDelegate BrowseResult;



        public UPnP_SourceFunctions(CpConnectionManager CM, CpContentDirectory CD)
        {
            _ConnectionManager = CM;
            _ContentDirectory = CD;
        }

        public void Browse(string s)
        {
            _ContentDirectory.Browse(s, CpContentDirectory.Enum_A_ARG_TYPE_BrowseFlag.BROWSEDIRECTCHILDREN, "*", 0, 0, "", "", Callback);

        }

        private void Callback(CpContentDirectory sender, string objectId, CpContentDirectory.Enum_A_ARG_TYPE_BrowseFlag browseFlag, string filter, uint startingIndex, uint requestedCount, string sortCriteria, string result, uint numberReturned, uint totalMatches, uint updateId, UPnPInvokeException upnPInvokeException, object tag)
        {
            BrowseResult(result, null);
        }
    }
}

