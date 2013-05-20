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
using playerlayout.Properties;
using TemplateSync;

namespace playerlayout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool play = new bool();
        private Settings settingsw;

        private UPnP_SinkFunctions _UPnPSink = null;
        private UPnP_SourceFunctions _UPnPSource = null;
        private UPnP_Setup setup;

        public MainWindow()
        {
            InitializeComponent();
            settingsw = new Settings();
            
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

        private void ButtonX_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonM_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Full Skærm");
        }

        private void ButtonMini_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("minimize window");
        }

        private void Playbutton_OnClick(object sender, RoutedEventArgs e)
        {
            

            if (play)
            {
                pap.Source = new BitmapImage(new Uri("play.png", UriKind.Relative));
                play = false;

                if (_UPnPSink != null)
                    _UPnPSink.Play();
            }


            else
            {
                pap.Source = new BitmapImage(new Uri("Pause.png", UriKind.Relative));
                play = true;

                if (_UPnPSink != null)
                    _UPnPSink.Pause();
            }
            
            
        }

        private void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            settingsw = new Settings();
            settingsw.Show();

        }

        private void BtnNext_OnClick(object sender, RoutedEventArgs e)
        {
            if (_UPnPSink != null)
                _UPnPSink.Next();
        }

        private void BtnPrevious_OnClick(object sender, RoutedEventArgs e)
        {
            if (_UPnPSink != null)
                _UPnPSink.Previous();
        }


        private void BtnStop_OnClick(object sender, RoutedEventArgs e)
        {
            if (_UPnPSink != null)
                _UPnPSink.Stop();
        }
    }
}
