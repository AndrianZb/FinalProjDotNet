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
        private List<ContactsCreator> contacts = new List<ContactsCreator>();
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
          


            openFileDialog.Filter = "CSV files (*.csv;*.txt)|*.csv;*.txt|All Files (*.*)|*.*";
            //should we add specification for initial directory? (Not required)
            
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.RestoreDirectory = true;

 
            if (openFileDialog.ShowDialog() == true)
            {
                var fileContent = string.Empty;
                //var myText = new StreamReader(openFileDialog.FileName);
                Console.WriteLine("test");
                //test.Content = myText;
                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    char delims = '\n';
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //test.Content = line;
                        List<string> data = new List<string>();

                    data.Add(line.Split(',')[0]);
                    data.Add(line.Split(',')[1]);
                    data.Add(line.Split(',')[2]);
                    data.Add(line.Split(',')[3]);
                   
                   
                    test1.Content = data[0];
                    test2.Content = data[3];
                    contacts.Add(new ContactsCreator(data[0], data[1], data[2], data[3]));
                    counter++;
                }
                
                //foreach(var x in contacts)
                //{
                //    test1.Content=data[0];
                   

                //}
                
                //test.Content = contacts[0].ToString();
                //test1.Content = contacts[1].ToString();
                //test2.Content = contacts[2].ToString();


            }



        }
      

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            //title would be good.
            //validate names - > put constraints 
           
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files | *.csv;*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, "Line 1\r\nLine2");
            }

        }

        private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
