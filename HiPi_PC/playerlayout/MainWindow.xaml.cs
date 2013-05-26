using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FileSender;
using UPnP_CP;
using playerlayout.Properties;
using TemplateSync;
using Containers;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace playerlayout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool play = new bool();
        private Settings settingsw;
        
        //private UPnPHandler _UPnPHandler = new UPnPHandler();

        public MusicIndexToGui musikindex = new MusicIndexToGui();
        public PlayQueueToGui playqueue = new PlayQueueToGui();

        private UPnP_Setup _UPnPSetup;
        private ISinkFunctions _UPnPSink = null;
        private ISourceFunctions _UPnPSource = null;

        
        public MainWindow()
        {
            InitializeComponent();
            settingsw = new Settings();

            _UPnPSetup = new UPnP_Setup();

            subscribe();

            _UPnPSetup.StartServices();
            
            dgPlayQueue.ItemsSource = playqueue;
            dgMusikindex.ItemsSource = musikindex;
            dgPlayQueue.IsReadOnly = true;
            dgMusikindex.IsReadOnly = true;

            //todo: greyout buttons
            btnNext.IsEnabled = false;
            //btnNext.IsEnabled = true;
            btnPrevious.IsEnabled = false;
            btnStop.IsEnabled = false;
            btnPlayPause.IsEnabled = false;
            sliderTime.IsEnabled = false;
            sliderVol.IsEnabled = false;
            btnSync.IsEnabled = false;
        }

        public void subscribe()
        {
            _UPnPSetup.AddSinkEvent += getUPnPSink;
            _UPnPSetup.AddSourceEvent += getUPnPSource;
        }

        public void getUPnPSink(UPnP_SinkFunctions e, EventArgs s)
        {
            _UPnPSink = e;

            _UPnPSink.getIPEvent += UpnPSinkOnGetIpEvent;
            _UPnPSink.getPositionEvent += UpnPSinkOnGetPositionEvent;
            _UPnPSink.getVolEvent += UpnPSinkOnGetVolEvent;
            _UPnPSink.transportStateEvent += UpnPSinkOnTransportStateEvent;

            //todo: degrey buttons
            Dispatcher.BeginInvoke(new Action(() =>
                {
                    btnNext.IsEnabled = true;
                    btnPrevious.IsEnabled = true;
                    btnStop.IsEnabled = true;
                    btnPlayPause.IsEnabled = true;
                    sliderTime.IsEnabled = true;
                    sliderVol.IsEnabled = true;
                    btnSync.IsEnabled = true;
                }));
            
        }

        public void getUPnPSource(UPnP_SourceFunctions e, EventArgs s)
        {
            _UPnPSource = e;

            _UPnPSource.BrowseResult += UpnPSourceOnBrowseResult;

            //todo: degrey buttons
        }

        private void UpnPSourceOnBrowseResult(object sender, List<ITrack> tracks)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (tracks[0].ParentID == "all")
                {
                    playqueue.Clear();

                    foreach (var track in tracks)
                    {
                        playqueue.Add(track);
                    }   
                }
                else if (tracks[0].ParentID == "playqueue")
                {
                    musikindex.Clear();

                    foreach (var track in tracks)
                    {
                        musikindex.Add(track);
                    }
                }
            }));
        }

        private void UpnPSinkOnTransportStateEvent(object sender, EventArgsContainer<string> eventArgsContainer)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                switch (eventArgsContainer._data)
                {
                    case "PLAYING":
                        togglePlayButton();
                        break;
                    case "STOPPED":
                        togglePlayButton();
                        break;
                    case "BROWSE":

                        break;
                    default:
                        break;
                }
            }));
        }

        private void UpnPSinkOnGetVolEvent(object sender, EventArgsContainer<ushort> eventArgsContainer)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (eventArgsContainer._data < 70)
                {
                    sliderVol.Value = 70;
                }
                else
                {
                    sliderVol.Value = Convert.ToDouble(eventArgsContainer._data);
                }
            }));
        }

        private void UpnPSinkOnGetPositionEvent(object sender, EventArgsContainer<List<ushort>> eventArgsContainer)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                sliderTime.Value = eventArgsContainer._data[0];
                sliderTime.Maximum = eventArgsContainer._data[1];

            }));
        }

        private void UpnPSinkOnGetIpEvent(object sender, EventArgsContainer<string> eventArgsContainer)
        {
            var thread = new Thread(() =>
            {
                var dlg = new OpenFileDialog();
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Client cli = new Client(dlg.FileName, eventArgsContainer._data);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        

        private void Playbutton_OnClick(object sender, RoutedEventArgs e)
        {
            //togglePlayButton();

            _UPnPSink.Pause();
        }

        private void togglePlayButton()
        {
            if (play)
            {
                pap.Source = new BitmapImage(new Uri("Icons/Play.ico", UriKind.Relative));
                play = false;
            }

            else
            {
                pap.Source = new BitmapImage(new Uri("Icons/Pause.ico", UriKind.Relative));
                play = true;
            }
        }

        private void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            settingsw = new Settings();
            settingsw.Show();
        }

        private void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            _UPnPSink.Next();
        }

        private void BtnPrevious_OnClick(object sender, RoutedEventArgs e)
        {
            _UPnPSink.Previous();
        }


        private void BtnStop_OnClick(object sender, RoutedEventArgs e)
        {
            _UPnPSink.Stop();
        }

        private void SendFile_OnClick(object sender, RoutedEventArgs e)
        {
            _UPnPSink.GetIpAddress();
        }

        private void SyncButtonClick(object sender, RoutedEventArgs e)
        {
            //settingsw.Sync.SyncPiDb();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void DgMusikindex_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var result = dgMusikindex.SelectedItem;

            //todo: switch these two
            //observerHandler.SetNextAVTransportURI((ITrack)result);
            _UPnPSink.SetTransportURI((ITrack)result);
        }

        private void DgPlayQueue_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var result = dgPlayQueue.SelectedItem;

            _UPnPSink.SetTransportURI((ITrack)result);
        }

        private void BtnRescan_OnClick(object sender, RoutedEventArgs e)
        {
            _UPnPSetup = null;
            _UPnPSetup = new UPnP_Setup();
        }


        private void SliderVol_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sliderVol.Value == 70)
            {
                _UPnPSink.SetVolume(Convert.ToUInt16(0));    
            }
            else
            {
                _UPnPSink.SetVolume(Convert.ToUInt16(sliderVol.Value));    
            }
            
        }

        private void SliderTime_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _UPnPSink.SetPosition((ushort) Convert.ToInt16(sliderTime.Value));
        }
    }
}
