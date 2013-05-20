using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Containers
{
    class PlayQueueToGui : ObservableCollection<ITrack>
    {
        private void updateplayqeue(List<ITrack> listen)
        {
            this.Clear();

            foreach (var track in listen)
            {
                Add(track);
            }

        }


    }
}
