using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateSync
{
    public abstract class ISynchronizer
    {
        public abstract void Startup();
        public abstract void SyncLocalDb(List<string> pahts);
        public abstract void SyncPiDb();
    }
}
