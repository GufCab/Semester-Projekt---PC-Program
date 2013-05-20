using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Containers
{
    public class PlayQueueToGui : ObservableCollection<ITrack>
    {
        public void updateplayqeue(List<ITrack> listen)
        {
            this.Clear();

            foreach (var track in listen)
            {
                Add(track);
            }

        }


    }
}
