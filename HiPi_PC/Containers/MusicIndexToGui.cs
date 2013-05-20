using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Containers
{
    public class MusicIndexToGui : ObservableCollection<ITrack>
    {
        public void UpdateMusicindex(List<ITrack> listen)
        {
            this.Clear();

            foreach (var track in listen)
            {
                Add(track);
            }

        }
    }
}
