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
using UPnP_CP;
using XMLReader;
using Containers;
using System.IO;

namespace UPnP_CP_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UPnP_SinkFunctions _UPnPSink = null;
        private UPnP_SourceFunctions _UPnPSource = null;

        private UPnP_Setup setup;

        public MainWindow()
        {
            setup = new UPnP_Setup();

            InitializeComponent();

            subscribe();
            setup.StartSinkDisco();
            setup.StartSourceDisco();

            //btnSubscribe.IsEnabled = false;
        }

        public void subscribe()
        {
            setup.AddSinkEvent += getUPnPSink;
            setup.AddSourceEvent += getUPnPSource;
        }

        public void getUPnPSink(UPnP_SinkFunctions e, EventArgs s)
        {
            _UPnPSink = e;
            MessageBox.Show("Sink added");
        }

        public void getUPnPSource(UPnP_SourceFunctions e, EventArgs s)
        {
            _UPnPSource = e;
            MessageBox.Show("Source added");
        }

        private void btnPlayInvoke_Click(object sender, RoutedEventArgs e)
        {
            if(_UPnPSink != null)
                _UPnPSink.Play();
        }

        private void btnPauseInvoke_Click(object sender, RoutedEventArgs e)
        {
            if (_UPnPSink != null)
                _UPnPSink.Pause();
        }

        private void btnStopInvoke_Click(object sender, RoutedEventArgs e)
        {
            if (_UPnPSink != null)
                _UPnPSink.Stop();
        }

        private void btnNextInvoke_Click(object sender, RoutedEventArgs e)
        {
            if (_UPnPSink != null)
                _UPnPSink.Next();
        }

        private void btnPreviousInvoke_Click(object sender, RoutedEventArgs e)
        {
            if (_UPnPSink != null)
                _UPnPSink.Previous();
        }

        private void btnSetTransportURI_Click(object sender, RoutedEventArgs e)
        {
            if (_UPnPSink != null)
            {
                _UPnPSink.SetTransportURI(TransPath.Text, "");
            }
        }

        private void btnSubscribe_Click(object sender, RoutedEventArgs e)
        {
           //Probably not needed
        }
    }
}
