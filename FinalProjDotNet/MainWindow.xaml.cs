using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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

        // Load the data from the database into the datagrid
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            // open file window pops up to select a file
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "CSV files (*.csv;*.txt)|*.csv;*.txt|All Files (*.*)|*.*";
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.RestoreDirectory = true;

            //  checks if a file was selected
            if (openFileDialog.ShowDialog() == true)
            {
                // clears the data from the contacts list to fill it with new items
                if (contacts.Any())
                {
                    contacts.Clear();
                }
                var fileStream = openFileDialog.OpenFile();

                // Read file (.csv or .txt)
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    //Seperate lines of the file
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

                        //add data to the contacts list
                        contacts.Add(new ContactsCreator() { FirstName = data[0], LastName = data[1], PhoneNum = data[2], Email = data[3] });
                    }
                }
            }

            // asks the user if he wants to add the data from his file or override the curent data
            MessageBoxResult result = MessageBox.Show("Would you like to add the data to the database instead of overriding the current data?", "", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.No:
                    DBC.DeleteAndUpdate(contacts);
                    break;
                case MessageBoxResult.Yes:
                    foreach (var x in contacts)
                    {
                        string[] arr = { x.FirstName, x.LastName, x.PhoneNum, x.Email };
                        DBC.Add(arr);
                    }
                    break;
            }
            UpdateData();
        }


        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            //opens a window where the user can save the current fields odf the datagrid in a csv document
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
                foreach (var x in DBC.getData())
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
            View();
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            View();
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
            foreach (ContactsCreator c in row)
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
                editWindow.Close();
            }
        }

        // updates the data in the dataGrid with the new data from the DataBase
        public void UpdateData()
        {
            contacts = DBC.getData();
            myDataGrid.ItemsSource = null;
            myDataGrid.ItemsSource = contacts;
        }

        public void View()
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
    }
}
