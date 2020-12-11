using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
        // list for fields of the wpf / database
        private List<ContactsCreator> contacts = new List<ContactsCreator>();
        //private List<string> viewInfo = new List<string>();

        
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (contacts.Any())
            {
                contacts.Clear();
            }
            
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "CSV files (*.csv;*.txt)|*.csv;*.txt|All Files (*.*)|*.*";
            openFileDialog.InitialDirectory = "C:\\Users\\mihaj\\Desktop\\debugProjectFolder";
            //openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.RestoreDirectory = true;

            // Read file (.csv or .txt)
            if (openFileDialog.ShowDialog() == true)
            {

                var fileStream = openFileDialog.OpenFile();

                // Seperate lines of the file
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    char delims = '\n';
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        //seperate each field of data and input into a list
                        List<string> data = new List<string>();

                        data.Add(line.Split(',')[0]);
                        data.Add(line.Split(',')[1]);
                        data.Add(line.Split(',')[2]);
                        data.Add(line.Split(',')[3].Split(delims)[0]);

                        contacts.Add(new ContactsCreator() { FirstName = data[0], LastName = data[1], PhoneNum = data[2], Email = data[3] });
                        //openFileDialog.Dispose();
                    }
                }
            }
            
            //link WPF with list
            myDataGrid.ItemsSource = null;
            myDataGrid.ItemsSource = contacts;
            }
            
        
        

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "CSV files (*.csv;*.txt)|*.csv;*.txt|All Files (*.*)|*.*";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.InitialDirectory = "C:\\";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.DefaultExt = "csv";

            // reconstruct list and seperate every data with a comma. At the end of each ContactsCreator object, skip a line
            if (saveFileDialog.ShowDialog() == true)
            {
                string text = "";
                foreach(var x in contacts)
                {
                    text += x.FirstName.ToString();
                    text += ",";
                    text += x.LastName.ToString();
                    text += ",";
                    text += x.PhoneNum.ToString();
                    text += ",";
                    text += x.Email.ToString();
                    text += "\n";
                }
                // write all to file and save it on the computer
                File.WriteAllText(saveFileDialog.FileName, text);
            }
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            PopUpView secondWindow = new PopUpView();
            //PopUpView secondWindow = new PopUpView(viewInfo[0], viewInfo[1], viewInfo[2], viewInfo[3], viewInfo[4]);
            secondWindow.Show();
        }

        //trying to make a row selector !!!!!!!!!!!
        //private void selectRow_Checked(object sender, RoutedEventArgs e)
        //{
            
        //    var checker = sender as RadioButton;
        //    //if(checker.IsChecked == true)
        //    var item = checker.DataContext as DataRowView;
        //    Object[] column = item.Row.ItemArray;
        //    test.Content = column[0].ToString();

        //    if (column[0].ToString() != viewInfo[0])
        //    {
        //        for (int i = 0; i < 4; i++)
        //        {
        //            viewInfo.Add(column[i].ToString());
        //            viewInfo.Add(column[i].ToString());
        //            viewInfo.Add(column[i].ToString());
        //            viewInfo.Add(column[i].ToString());
        //        }
        //    }
        //}

        
    }
}
