using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateSync
{
    public interface ISynchronizer
    {
        void Startup();
        void SyncLocalDb(List<string> pahts);
        void SyncPiDb();
    }
}
