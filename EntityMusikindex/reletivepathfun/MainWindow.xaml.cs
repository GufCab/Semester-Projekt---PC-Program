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
using System.Windows.Forms;

namespace reletivepathfun
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
            folderBrowserDialogButton2.Click += new RoutedEventHandler(folderBrowserDialogButton2_Click);
        }

        private string pat1;
        private string pat2;

        private void folderBrowserDialogButton_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            string prefix = "Folder Browser Dialog: ";
            if (((string) folderBrowserDialogButton.Content).Length > prefix.Length)
            {
                dlg.SelectedPath = ((string) folderBrowserDialogButton.Content).Substring(prefix.Length);
                pat1 = dlg.SelectedPath;
            }

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderBrowserDialogButton.Content = prefix + dlg.SelectedPath;
            }

        }


        private void folderBrowserDialogButton2_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            string prefix = "Folder Browser Dialog: ";
            if (((string)folderBrowserDialogButton2.Content).Length > prefix.Length)
            {
                dlg.SelectedPath = ((string)folderBrowserDialogButton2.Content).Substring(prefix.Length);
                pat2 = dlg.SelectedPath;
            }

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderBrowserDialogButton2.Content = prefix + dlg.SelectedPath;
            }

        }

        private void Reletive_OnClick(object sender, RoutedEventArgs e)
        {
            reletive.Content = 
            
        }

    }
}
