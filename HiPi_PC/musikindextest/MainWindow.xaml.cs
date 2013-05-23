using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XMLReader;
using System.Collections.ObjectModel;
using XMLHandler;
using Containers;


namespace musikindextest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
  /*      private ObervHandler _hand;
        public ObservableCollection<ITrack> musikindex = new ObservableCollection<ITrack>();
        public ObservableCollection<ITrack> playqueue = new ObservableCollection<ITrack>();

        public MainWindow()
        {
            InitializeComponent();
            
            _hand = new ObervHandler();
            _hand.musikUpdateEvent += HandOnMusikUpdateEvent;

            var fly = new Track();

            fly.Album = "fdfd";
            fly.Artist = "haha";
            fly.DeviceIP = "hahahahahaah";
            fly.ParentID = "1";
            var list = new List<ITrack>();
            list.Add(fly);

            //_hand.UpdateMusicindex(list);

            Grid.ItemsSource = musikindex;

            trackEventArgs t = new trackEventArgs(list);

            //HandOnMusikUpdateEvent(_hand, t);
            
            
        }

        private void HandOnMusikUpdateEvent(object o, trackEventArgs tracks)
        {
            Dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach (var track in tracks._tracks)
                    {
                        musikindex.Add(track);
                    }
                }));

        }

*/
    }
}
