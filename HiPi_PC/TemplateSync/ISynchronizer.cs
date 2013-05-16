using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateSync
{
    public interface ISynchronizer
    {
        void Startup(); // denne skal kaldes ved opstart
        // giver til kende at enheden er tændt og starter for Live555
        void SyncLocalDb(List<string> pahts); // Kaldes når man gerne vil opdatere sin lokale musik til local db.
        void SyncPiDb(); // kaldes når man ønsker at tilføje ny musik til pien.
    }
}
