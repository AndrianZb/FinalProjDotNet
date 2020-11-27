using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace FinalProjDotNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
          


            openFileDialog.Filter = "CSV files (*.csv;*.txt)|All Files (*.*)";
            //should we add specification for initial directory? (Not required)
            //openFileDialog.InitialDirectory = @"insertdirectoryhere";
          
            if (openFileDialog.ShowDialog() == true)
            {

                var myText = File.ReadAllText(openFileDialog.FileName);
            }

          


        }
      

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            //maybe add filter later
            //title would be good.
            //validate names - > put constraints 

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, "Line 1\r\nLine2");
            }

        }
    }
}
