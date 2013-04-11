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
using playerlayout.Properties;

namespace playerlayout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool play = new bool();
        Settings settingsw = new Settings();
        public MainWindow()
        {
            InitializeComponent();
            
            
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
            }


            else
            {
                pap.Source = new BitmapImage(new Uri("Pause.png", UriKind.Relative));
                play = true;
            }
            
            
        }

        private void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            
            settingsw.Show();

        }
    }
}
