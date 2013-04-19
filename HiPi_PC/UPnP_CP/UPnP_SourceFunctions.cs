using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceStack;

namespace UPnP_CP
{
    public interface ISourceFunctions
    {
        string Browse(string id);
    }

    public class UPnP_SourceFunctions : ISourceFunctions
    {
        private SourceStack.CpConnectionManager _ConnectionManager;
        private SourceStack.CpContentDirectory _ContentDirectory;

        public UPnP_SourceFunctions(CpConnectionManager CM, CpContentDirectory CD)
        {
            _ConnectionManager = CM;
            _ContentDirectory = CD;
        }

        public string Browse(string s)
        {
            //_ContentDirectory.Browse();
            return s;
        }
    }
}

