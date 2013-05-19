using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace dbclases
{
    public interface IPidbhandel
    {
        void Markasonline();

        void SyncfromLocalToPI();

        void Markasofline();
    }


   public class PiDbhandel : IPidbhandel
    {



        public void Markasonline()
        {
            if (CheckIfDeviceExists())
            {
               // her skal der kigges om deviceuuid og pi stemmer over ens
               // hvis det ikke gør skal det rettes

            }

        }

        public void SyncfromLocalToPI()
        {
            List<filepath> pathtopi;

            using (var musik = new pcindexEntities())
            {
                pathtopi = (from p in musik.filepaths select p).ToList();

            }
            using (var pimusik = new piindexEntities())
            {
                foreach (var ele in pathtopi)
                {
                    var fil = new filepath();
                    fil.Device_UUIDDevice = ele.Device_UUIDDevice;
                    fil.FilePath1 = ele.FilePath1;
                    fil.UUIDPath = ele.UUIDPath;

                    pimusik.filepaths.Add(fil);
                    pimusik.SaveChanges();
                }
            }

        }

        public void Markasofline()
        {
            throw new NotImplementedException();
        }


        private bool CheckIfDeviceExists()
        {
            return true;
        }
    }
}
