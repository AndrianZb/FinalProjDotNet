using Microsoft.Win32;
using System;
using System.Collections;
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
        // list for fields of the wpf / database
        List<ContactsCreator> contacts = new List<ContactsCreator>();
        DBConnection DBC = DBConnection.instance;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();

        }
        //private List<string> viewInfo = new List<string>();

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (contacts.Any())
            {
                contacts.Clear();
            }
            
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "CSV files (*.csv;*.txt)|*.csv;*.txt|All Files (*.*)|*.*";
            //openFileDialog.InitialDirectory = "C:\\Users\\mihaj\\Desktop\\debugProjectFolder";
            openFileDialog.InitialDirectory = "C:\\";
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
                        DBC.DeleteAndUpdate(contacts);

                        UpdateData();
                    }
                }
            }
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
                foreach(var x in DBC.getData())
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
            IList row = myDataGrid.SelectedItems;
            
            foreach (ContactsCreator c in row)
            {
                secondWindow.firstName.Content = c.FirstName;
                secondWindow.lastName.Content = c.LastName;
                secondWindow.phoneNumber.Content = c.PhoneNum;
                secondWindow.email.Content = c.Email;
            }
            secondWindow.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            PopUpAdd addWindow = new PopUpAdd();
            addWindow.Show();
            UpdateData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            IList row = myDataGrid.SelectedItems;
            foreach(ContactsCreator c in row)
            {
                DBC.Delete(c.Id);
            }
            UpdateData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            PopUpEdit editWindow = new PopUpEdit();
            editWindow.idEdit.Text = null;
            editWindow.fNameEdit.Text = null;
            editWindow.lNameEdit.Text = null;
            editWindow.pNumEdit.Text = null;
            editWindow.emailEdit.Text = null;

            IList row = myDataGrid.SelectedItems;
            foreach (ContactsCreator c in row)
            {
                editWindow.idEdit.Text = c.Id.ToString();
                editWindow.fNameEdit.Text = c.FirstName;
                editWindow.lNameEdit.Text = c.LastName;
                editWindow.pNumEdit.Text = c.PhoneNum;
                editWindow.emailEdit.Text = c.Email;
            }

            //check if any row was selected at all
            if (editWindow.fNameEdit.Text != "")
            {
                editWindow.Show();
            }
            else
            {
                MessageBox.Show(" No rows were selected!", "WARNING");
            }
        }

        // updates the data in the dataGrid with the new data from the DataBase
        public void UpdateData()
        {
            contacts = DBC.getData();
            myDataGrid.ItemsSource = null;
            myDataGrid.ItemsSource = contacts;
        }

        //bool TestForNullOrEmpty(string s)
        //{
        //    bool result;
        //    result = s == null || s == string.Empty;
        //    return result;
        //}
    }
}
