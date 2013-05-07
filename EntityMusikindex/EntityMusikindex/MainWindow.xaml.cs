using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using dbclases;


namespace EntityMusikindex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            folderBrowserDialogButton.Click += new RoutedEventHandler(folderBrowserDialogButton_Click);




        }
        DBshow db = new DBshow();

        public void fillliste()
        {

        }


        private async void ButtonUpdata_OnClick(object sender, RoutedEventArgs e)
        {
            var slowtast = Task.Factory.StartNew(()=> db.Showallmusic());
            await slowtast;
            INDEX.ItemsSource = db;


            //using (var musik = new musikindexEntities())
            //{

            //   INDEX.ItemsSource = (from p in musik.musikdatas select new {Title = p.Title , Artist = p.Artist_idArtist , Album = p.Album_idAlbum , Genre = p.Genre_Genre }).ToList();

            //    List<filepath> allPath = (from o in musik.filepaths select o).ToList();
            //    Path.ItemsSource = allPath;
            //}
        }

        void folderBrowserDialogButton_Click(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            string prefix = "Folder Browser Dialog: ";
            if (((string)folderBrowserDialogButton.Content).Length > prefix.Length)
            {
                dlg.SelectedPath = ((string)folderBrowserDialogButton.Content).Substring(prefix.Length);
            }

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderBrowserDialogButton.Content = prefix + dlg.SelectedPath;
            }



        }


        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
        }



        private void Buttonremoveall_OnClick(object sender, RoutedEventArgs e)
        {
            using (var musik = new musikindexEntities())
            {
                musik.Database.Delete();

            }
        }

       
    }
}
