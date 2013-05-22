using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Containers
{
    public class PlayQueueToGui : ObservableCollection<ITrack>
    {
        public PlayQueueToGui()
        {
            if ((bool) (DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof (DependencyObject)).DefaultValue))
            {
                var temptrack = new Track();
                temptrack.Album = "hans";
                temptrack.Artist = "guisis";
                temptrack.Path = "jeg stener";

                // In Design mode
                Add(temptrack);
            }
        }

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
