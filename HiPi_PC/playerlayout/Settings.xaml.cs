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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using TemplateSync;
using Application = System.Windows.Application;

namespace constant
{
    public static class Constants
    {
        public const double Size = 5;
    }

}
namespace playerlayout
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>

    public partial class Settings : Window
    {
        public SyncTemplate Sync = new SyncTemplate();
        class DialogData
        {
            private string reportFolder;
            
            public string ReportFolder
            {
                get { return reportFolder; }
                set { reportFolder = value; }
            }
        }

        DialogData data = new DialogData();

        public string ReportFolder
        {
            get { return data.ReportFolder; }
            set { data.ReportFolder = value; }
        }

        public Settings()
        {
            InitializeComponent();


            //AddButton.Click += new RoutedEventHandler(openFileDialogButton_Click);

            AddButton.Click += AddButton_Click;
            RemoveButton.Click += RemoveButton_Click;

            skins_initialize();
        }

        void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            int index = PathFolderListBox.SelectedIndex;
            if (index >= 0)
                {
                PathFolderListBox.Items.RemoveAt(index);
                }
            }

        private void skins_initialize()
        {
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

        void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //SHOULD CONTROL FOR DUPLICATED ITEMS IN THE FOLDER.
            System.Windows.Forms.FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = ReportFolder;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReportFolder = dlg.SelectedPath;
                PathFolderListBox.Items.Add(ReportFolder);
            }

            //string prefix = "Open File Dialog: ";
        }
        //public SyncTemplate Sync = new SyncTemplate();
        private void SyncronizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            List<string> pathes = new List<string>();

            for (int i = 0; i < PathFolderListBox.Items.Count; ++i)
            {
                pathes.Add(PathFolderListBox.Items.GetItemAt(i).ToString());

            }
            
            Sync.SyncLocalDb(pathes);

            Sync.SyncPiDb();
        }
        
        
    }
}
        


    