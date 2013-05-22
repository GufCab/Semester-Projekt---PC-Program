using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;


namespace Containers
{
    public class MusicIndexToGui : ObservableCollection<ITrack>
    {
        public MusicIndexToGui()
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


    }
}
