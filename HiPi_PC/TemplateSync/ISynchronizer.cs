using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateSync
{
    /// <summary>
    /// this is the interface used to synchronize
    /// </summary>
    public interface ISynchronizer
    {
        void Startup();
        void SyncLocalDb(List<string> pahts);
        void SyncPiDb();
    }
}
