﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
using System.Windows.Threading;
using FileSender;
using UPnP_CP;
using playerlayout.Annotations;
using playerlayout.Properties;
using TemplateSync;
using Containers;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

/// <summary>
/// this Namespace contains UI.
/// </summary>

namespace playerlayout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool play = new bool();
        private Settings settingsw;

        public ObservableCollection<ITrack> musikindex = new ObservableCollection<ITrack>();
        public ObservableCollection<ITrack> playqueue = new ObservableCollection<ITrack>();

        private UPnP_Setup _UPnPSetup;
        private ISinkFunctions _UPnPSink = null;
        private ISourceFunctions _UPnPSource = null;

        //private System.Timers.Timer _sliderTimer = new System.Timers.Timer();
        private DispatcherTimer _sliderTimer = new DispatcherTimer();

        private double _time;

        public double Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }

        /// <summary>
        /// MainWindow Codebehind
        /// </summary>
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

            sliderTime.DataContext = this;
            _sliderTimer.Interval = TimeSpan.FromMilliseconds(500);
            _sliderTimer.Tick += new EventHandler(timerEventFunc);
            sliderTime.Maximum = 0;
        }

        private double t;

        private void timerEventFunc(object sender, EventArgs elapsedEventArgs)
        {
            Time += 0.5;
            t += 0.5;

            if (Time >= sliderTime.Maximum)
            {
                Time = 0;
                _UPnPSink.GetPosition();
            }

            if (t > 5)
            {
                t = 0;
                _UPnPSink.GetPosition();
                _UPnPSource.Browse("playqueue");
                _UPnPSource.Browse("all");
                _UPnPSink.GetVolume();
            }
        }

        /// <summary>
        /// Function to greyout buttons when a device isn't detected
        /// </summary>
        public void GreyoutButtons()
        {
            btnNext.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnPlayPause.IsEnabled = false;
            sliderTime.IsEnabled = false;
            sliderVol.IsEnabled = false;
            btnSync.IsEnabled = false;
        }

        /// <summary>
        /// Subscribe to device-detection events
        /// </summary>
        public void subscribe()
        {
            _UPnPSetup.AddSinkEvent += getUPnPSink;
            _UPnPSetup.AddSourceEvent += getUPnPSource;
        }

        /// <summary>
        /// EventFunction that adds upnp sink and subscribes to sink events
        /// </summary>
        /// <param name="e">The sink device that is discovered</param>
        /// <param name="s"></param>
        public void getUPnPSink(ISinkFunctions e, EventArgs s)
        {
            _UPnPSink = e;
            _sliderTimer.Start();

            _UPnPSink.getIPEvent += UpnPSinkOnGetIpEvent;
            _UPnPSink.getPositionEvent += UpnPSinkOnGetPositionEvent;
            _UPnPSink.getVolEvent += UpnPSinkOnGetVolEvent;
            _UPnPSink.transportStateEvent += UpnPSinkOnTransportStateEvent;

            Dispatcher.BeginInvoke(new Action(() =>
                {
                    btnNext.IsEnabled = true;
                    btnPrevious.IsEnabled = true;
                    btnPlayPause.IsEnabled = true;
                    sliderTime.IsEnabled = true;
                    sliderVol.IsEnabled = true;
                    btnSync.IsEnabled = true;
                }));
        }

        /// <summary>
        /// EventFunction that adds upnp source and subscribes to source events
        /// </summary>
        /// <param name="e">The source device that is discovered</param>
        /// <param name="s"></param>
        public void getUPnPSource(ISourceFunctions e, EventArgs s)
        {
            _UPnPSource = e;

            _UPnPSource.BrowseResult += UpnPSourceOnBrowseResult;
        }

        /// <summary>
        /// Eventfunction that is fired when the browse command returns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tracks">The list of tracks that is returned from the UPnP device</param>
        private void UpnPSourceOnBrowseResult(object sender, List<ITrack> tracks)
        {
            if (tracks.Count > 0)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                    {
                        if (tracks[0].ParentID == "all")
                        {
                            musikindex.Clear();

                            foreach (var track in tracks)
                            {
                                musikindex.Add(track);
                            }
                        }
                        else if (tracks[0].ParentID == "playqueue")
                        {
                            playqueue.Clear();

                            foreach (var track in tracks)
                            {
                                playqueue.Add(track);
                            }
                        }
                    }));
            }
        }

        /// <summary>
        /// Event that is fired when someone presses play/pause/next/previous or the song changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgsContainer">event parameter</param>
        private void UpnPSinkOnTransportStateEvent(object sender, string eventArgsContainer)
        {
            Dispatcher.BeginInvoke(new Action(() =>
                {
                    switch (eventArgsContainer)
                    {
                        case "PLAYING":
                            TogglePlayButton();
                            break;
                        case "STOPPED":
                            TogglePlayButton();
                            break;
                        case "BROWSE":
                            _UPnPSource.Browse("playqueue");
                            break;
                        default:
                            break;
                    }
                }));
        }

        /// <summary>
        /// Event that is fired when the GetVolume event returns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgsContainer">The volume of the player</param>
        private void UpnPSinkOnGetVolEvent(object sender, ushort eventArgsContainer)
        {
            Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (eventArgsContainer < 70)
                    {
                        sliderVol.Value = 70;
                    }
                    else
                    {
                        sliderVol.Value = Convert.ToDouble(eventArgsContainer);
                    }
                }));
        }

        /// <summary>
        /// Event that is fired when GetPosition returns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgsContainer">List with the current position and the duration of the song</param>
        private void UpnPSinkOnGetPositionEvent(object sender, List<ushort> eventArgsContainer)
        {
            Dispatcher.BeginInvoke(new Action(() =>
                {
                    sliderTime.Maximum = eventArgsContainer[1];
                    //sliderTime.Value = eventArgsContainer[0];
                    _time = (double) eventArgsContainer[0];
                }));
        }

        /// <summary>
        /// Event that is fired when GetIPAddress returns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgsContainer">IPAddress of the UPnP sink</param>
        private void UpnPSinkOnGetIpEvent(object sender, string eventArgsContainer)
        {
            var thread = new Thread(() =>
                {
                    var dlg = new OpenFileDialog();
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        AbstractFileSenderClient cli = new FileSenderClient(dlg.FileName, eventArgsContainer);
                    }
                });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            MessageBox.Show("File transfer done!");
        }

        /// <summary>
        /// Sends a pause command when someone pressed the play button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Playbutton_OnClick(object sender, RoutedEventArgs e)
        {
            _UPnPSink.Pause();
        }

        /// <summary>
        /// Toggles the image on the paly button between play and pause icon
        /// </summary>
        private void TogglePlayButton()
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

        /// <summary>
        /// Shows settings window when "settings" button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            settingsw = new Settings();
            settingsw.Show();
        }

        /// <summary>
        /// Sends a UPnP next command when a user pushes the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            _UPnPSink.Next();
        }

        /// <summary>
        /// Sends a UPnP previous command when a user pushes the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrevious_OnClick(object sender, RoutedEventArgs e)
        {
            _UPnPSink.Previous();
        }

        /// <summary>
        /// Sends a UPnP GetIPAddress command when a user pushes the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendFile_OnClick(object sender, RoutedEventArgs e)
        {
            if(_UPnPSink != null)
                _UPnPSink.GetIpAddress();
        }

        private void SyncButtonClick(object sender, RoutedEventArgs e)
        {
            settingsw.Sync.SyncPiDb();
        }

        /// <summary>
        /// Closes the threads and stuff to make a clean exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Adds a track to the musicindex on the UPnP device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgMusikindex_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var result = dgMusikindex.SelectedItem;

            //todo: switch these two
            if (result != null)
            {
                _UPnPSink.SetNextTransportURI((ITrack) result);

                //hack to ensure SetNextAVTransportURI is done and so you cant add tracks to fast
                Thread.Sleep(500);
                _UPnPSource.Browse("playqueue");
            }
        }

        /// <summary>
        /// Adds a track to the playQueue on the UPnP device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgPlayQueue_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var result = dgPlayQueue.SelectedItem;

            if (result != null)
            {
                _UPnPSink.SetTransportURI((ITrack) result);
            }
        }

        /// <summary>
        /// Terminates the UPnP connection and tries to make a new one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRescan_OnClick(object sender, RoutedEventArgs e)
        {
            _UPnPSetup = null;
            _UPnPSink = null;
            _UPnPSource = null;
            GreyoutButtons();
            _UPnPSetup = new UPnP_Setup();
            subscribe();
            _UPnPSetup.StartServices();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            var time = sliderTime.Value;

            //_UPnPSink.SetPosition((ushort) Convert.ToInt16(time));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DgMusikindex_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var result = dgMusikindex.SelectedItem;

            if (e.Key == Key.Return)
            {
                if (result != null)
                {
                    _UPnPSink.SetNextTransportURI((ITrack)result);

                    //hack to ensure SetNextAVTransportURI is done and so you cant add tracks to fast
                    Thread.Sleep(500);
                    _UPnPSource.Browse("playqueue");
                }
            }
        }


        private void DgPlayQueue_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var result = dgPlayQueue.SelectedItem;

            if (e.Key == Key.Return)
            {
                if (result != null)
                {
                    _UPnPSink.SetTransportURI((ITrack)result);
                }
            }
        }
    }
}