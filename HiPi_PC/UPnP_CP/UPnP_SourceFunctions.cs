using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceStack;

namespace UPnP_CP
{
    public class UPnP_SourceFunctions
    {
        private SourceStack.CpConnectionManager _ConnectionManager;
        private SourceStack.CpContentDirectory _ContentDirectory;

        public UPnP_SourceFunctions(CpConnectionManager CM, CpContentDirectory CD)
        {
            _ConnectionManager = CM;
            _ContentDirectory = CD;
        }
    }
}

