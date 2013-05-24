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
using XMLHandler;
using playerlayout.Properties;
using TemplateSync;
using Containers;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Containers;

namespace playerlayout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool play = new bool();
        private Settings settingsw;
        
        private ObervHandler observerHandler;

        public MusicIndexToGui musikindex = new MusicIndexToGui();
        public PlayQueueToGui playqueue = new PlayQueueToGui();
       

        public MainWindow()
        {
            InitializeComponent();
            settingsw = new Settings();
            observerHandler = new ObervHandler();

            observerHandler.musikUpdateEvent += HandOnMusikUpdateEvent;
            observerHandler.playQueueUpdateEvent += ObserverHandlerOnPlayQueueUpdateEvent;
            observerHandler.VolumeUpdateEvent += ObserverHandlerOnVolumeUpdateEvent;
            observerHandler.getIPEvent1 += ObserverHandlerOnGetIpEvent1;
            observerHandler.getPositionEvent += ObserverHandlerOnGetPositionEvent;
            observerHandler.transportStateEvent += ObserverHandlerOnTransportStateEvent;
            
            dgPlayQueue.ItemsSource = playqueue;
            dgMusikindex.ItemsSource = musikindex;
            dgPlayQueue.IsReadOnly = true;
            dgMusikindex.IsReadOnly = true;
        }

        private void ObserverHandlerOnTransportStateEvent(object sender, EventArgsContainer<string> eventArgsContainer)
        {
            Dispatcher.BeginInvoke(new Action(() =>
                {
                    switch (eventArgsContainer._data)
                    {
                        case "playing":
                            togglePlayButton();
                            break;
                        case "stopped":
                            togglePlayButton();
                            break;
                        default:
                            break;
                    }
                }));
            
        }

        private void ObserverHandlerOnGetPositionEvent(object sender, EventArgsContainer<List<ushort>> eventArgsContainer)
        {
            Dispatcher.BeginInvoke(new Action(() =>
                {
                    sliderTime.Value = eventArgsContainer._data[0];
                    sliderTime.Maximum = eventArgsContainer._data[1];
                    
                }));
        }

        private void ObserverHandlerOnGetIpEvent1(object sender, EventArgsContainer<string> eventArgsContainer)
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

        private void ObserverHandlerOnVolumeUpdateEvent(object o, EventArgsContainer<ushort> vol)
        {
            Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (vol._data < 70)
                    {
                        sliderVol.Value = 70;
                    }
                    else
                    {
                        sliderVol.Value = Convert.ToDouble(vol._data);
                    }
                }));
        }

        private void ObserverHandlerOnPlayQueueUpdateEvent(object o, EventArgsContainer<List<ITrack>> tracks)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                playqueue.Clear();

                foreach (var track in tracks._data)
                {
                    playqueue.Add(track);
                }
            }));
        }

        private void HandOnMusikUpdateEvent(object o, EventArgsContainer<List<ITrack>> tracks)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                musikindex.Clear();

                foreach (var track in tracks._data)
                {
                    musikindex.Add(track);
                }
            }));
        }

        private void Playbutton_OnClick(object sender, RoutedEventArgs e)
        {
            togglePlayButton();

            observerHandler.Pause();
        }

        private void togglePlayButton()
        {
            if (play)
            {
                pap.Source = new BitmapImage(new Uri("play.png", UriKind.Relative));
                play = false;
            }

            else
            {
                pap.Source = new BitmapImage(new Uri("Pause.png", UriKind.Relative));
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
            observerHandler.Next();
        }

        private void BtnPrevious_OnClick(object sender, RoutedEventArgs e)
        {
            observerHandler.Previous();
        }


        private void BtnStop_OnClick(object sender, RoutedEventArgs e)
        {
            observerHandler.Stop();
        }

        private void SendFile_OnClick(object sender, RoutedEventArgs e)
        {
            observerHandler.GetIPaddress();
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
            observerHandler.SetAVTransportURI((ITrack)result);

            //Dispatcher()
        }

        private void DgPlayQueue_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var result = dgPlayQueue.SelectedItem;

            observerHandler.SetAVTransportURI((ITrack)result);
        }

        private void BtnRescan_OnClick(object sender, RoutedEventArgs e)
        {
            observerHandler = null;
            observerHandler = new ObervHandler();
        }


        private void SliderVol_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sliderVol.Value == 70)
            {
                observerHandler.SetVolume(Convert.ToUInt16(0));    
            }
            else
            {
                observerHandler.SetVolume(Convert.ToUInt16(sliderVol.Value));    
            }
            
        }

        private void SliderTime_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            observerHandler.SetPosition((ushort) Convert.ToInt16(sliderTime.Value));
        }


    }
}
