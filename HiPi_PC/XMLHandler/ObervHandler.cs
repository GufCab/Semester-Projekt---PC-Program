using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Containers;
using XMLReader;


namespace XMLHandler
{
    class ObervHandler
    {
        XMLReaderPC xmlr = new XMLReaderPC();
        public MusicIndexToGui musicIndex = new MusicIndexToGui();
        public PlayQueueToGui playQueue = new PlayQueueToGui();
        private void Handle(string xml, int commando )
        {
            var list = xmlr.itemReader(xml);
            switch (commando)
            {
                case    0:
                playQueue.updateplayqeue(list);
                    break;
                case 1:
                    musicIndex.UpdateMusicindex(list);
                    break;
                default:
                break;

                    

            }
            list.Clear();

        }

    }
}
