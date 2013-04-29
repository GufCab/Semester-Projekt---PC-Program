﻿using System;
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


namespace EntityMusikindex
{
    /// <summary>
    /// Interaction logic fo    r MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            folderBrowserDialogButton.Click += new RoutedEventHandler(folderBrowserDialogButton_Click);
        }

        private void ButtonUpdata_OnClick(object sender, RoutedEventArgs e)
        {
            using (var musik = new musikindexEntities2())
            {
                
                INDEX.ItemsSource = (from p in musik.musikdata select new {Title = p.Titel , Kunstner = p.Kunstner , Album = p.Album , Genre = p.Genre }).ToList();


                List<filepathtabel> allPath = (from o in musik.filepathtabel select o).ToList();
                Path.ItemsSource = allPath;
            }
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
            
            var nummer = new musikdata();
            nummer.Titel = "haha";
            nummer.Album = "dans";
            nummer.Genre = "heavy";
            nummer.Kunstner = "Den glade";
            nummer.FilepathTabel_idFilesti = 1;

            var sti = new filepathtabel();
            sti.FilePath = "hehe";


            
            

            using (var musik = new musikindexEntities2())
            {
                  
               
           

               
                musik.musikdata.Add(nummer);

                musik.SaveChanges();

            }
        }

        private void Buttonremoveall_OnClick(object sender, RoutedEventArgs e)
        {
            using (var musik = new musikindexEntities2())
            {
                musik.Database.Delete();

            }
        }

       
    }
}
