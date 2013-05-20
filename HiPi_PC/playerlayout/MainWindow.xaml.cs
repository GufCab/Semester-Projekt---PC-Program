using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using XMLReader;
using MessageBox = System.Windows.MessageBox;

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


       

        public MainWindow()
        {
            InitializeComponent();
            settingsw = new Settings();
            observerHandler = new ObervHandler();

            //observerHandler.UpdateMusicindex(list);
            
            Musikindex.ItemsSource = observerHandler.musikindex;


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

                observerHandler.Play();
            }


            else
            {
                pap.Source = new BitmapImage(new Uri("Pause.png", UriKind.Relative));
                play = true;

               observerHandler.Pause();
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
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IClient cli = new Client();
                cli.Run(dlg.FileName);
            }

        }
    }
}
