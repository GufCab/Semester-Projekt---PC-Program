using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using Microsoft.Win32;


namespace playerlayout
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();

            openFileDialogButton.Click += new RoutedEventHandler(openFileDialogButton_Click);

            EnsureSkins();
            ApplySkin(NormalSkin);
            ChooseNormalSkin.Click += SkinChanged;
            ChooseZurgSkin.Click += SkinChanged;
            ChooseTerranSkin.Click += SkinChanged;
            ChooseProtossSkin.Click += SkinChanged;

        }

       

        private static ResourceDictionary ZurgSkin;
        private static ResourceDictionary NormalSkin;
        private static ResourceDictionary TerranSkin;
        private static ResourceDictionary ProtossSkin;

        private void EnsureSkins()
        {
            // this method is called each time a new Window1 is constructed,
            // so make sure we only load the resources the first time
            NormalSkin = new ResourceDictionary();
            NormalSkin.Source = new Uri("Styles/NormalSkin.xaml", UriKind.Relative);

            ZurgSkin = new ResourceDictionary();
            ZurgSkin.Source = new Uri("Styles/ZurgSkin.xaml", UriKind.Relative);

            TerranSkin = new ResourceDictionary();
            TerranSkin.Source = new Uri("Styles/TerranSkin.xaml", UriKind.Relative);

            ProtossSkin = new ResourceDictionary();
            ProtossSkin.Source = new Uri("Styles/ProtossSkin.xaml", UriKind.Relative);

        }

        private void SkinChanged(object o, EventArgs e)
        {

            if (ChooseZurgSkin.IsChecked.Value)
                ApplySkin(ZurgSkin);     
            else
            {
                if (ChooseTerranSkin.IsChecked.Value)
                    ApplySkin(TerranSkin);
                else
                {
                    if (ChooseProtossSkin.IsChecked.Value)
                    {
                        ApplySkin(ProtossSkin);
                    }
                    else
                    ApplySkin(NormalSkin);  
                }
                
            }
        }

        private void ApplySkin(ResourceDictionary newSkin)
        {
            Collection<ResourceDictionary> appMergedDictionaries =
                Application.Current.Resources.MergedDictionaries;

            // remove the old skins (MergedDictionary.Clear won't do the trick)
            if (appMergedDictionaries.Count != 0)
            {
                appMergedDictionaries.Remove(appMergedDictionaries[0]);
            }

            // add the new skin
            appMergedDictionaries.Add(newSkin);
        }

        void openFileDialogButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            string prefix = "Open File Dialog: ";
            if (((string)openFileDialogButton.Content).Length > prefix.Length)
            {
                dlg.FileName = ((string)openFileDialogButton.Content).Substring(prefix.Length);
            }

            if (dlg.ShowDialog() == true)
            {
                openFileDialogButton.Content = prefix + dlg.FileName;
                PathFolderTextBox.Text = dlg.FileName;
            }
        }
   }
    
}
        


    