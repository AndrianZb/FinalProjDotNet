using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FinalProjDotNet
{
    /// <summary>
    /// Interaction logic for PopUpEdit.xaml
    /// </summary>
    public partial class PopUpEdit : Window
    {
        List<ContactsCreator> contact = new List<ContactsCreator>();
        DBConnection DBC = DBConnection.instance;
        public PopUpEdit()
        {
            InitializeComponent();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            Regex phoneCheck = new Regex(@"^\d{10}$");
            Regex emailCheck = new Regex(@"^[^@\s]+@[^@\s\.]+\.[^@\.\s]+$");

            if (fNameEdit.Text.Length == 0)
            {
                MessageBox.Show("First name field cannot be empty!", "Alert");
            }
            else if (fNameEdit.Text.Length >= 50)
            {
                MessageBox.Show("First name field can only be 50 Characters maximum!", "Alert");
            }

            else if (lNameEdit.Text.Length == 0)
            {
                MessageBox.Show("Last name field cannot be empty!", "Alert");
            }
            else if (lNameEdit.Text.Length >= 50)
            {
                MessageBox.Show("Last name field can only be 50 Characters maximum!", "Alert");
            }

            else if (pNumEdit.Text.Length == 0)
            {
                MessageBox.Show("Phone number field cannot be empty!", "Alert");
            }
            else if (!phoneCheck.IsMatch(pNumEdit.Text))
            {
                MessageBox.Show("Phone number can only be 10 numbers maximum example: '1112223333'!", "Alert");
            }

            else if (emailEdit.Text.Length == 0)
            {
                MessageBox.Show("Email field cannot be empty!", "Alert");
            }
            else if (emailEdit.Text.Length >= 150)
            {
                MessageBox.Show("Email field can be 50 Characters maximum!", "Alert");
            }

            else if (!emailCheck.IsMatch(emailEdit.Text))
            {
                MessageBox.Show("Email has to be valid example: 'name@domain.com'!", "Alert");
            }
            else
            {
                int id = int.Parse(idEdit.Text);

                string[] arr = { fNameEdit.Text, lNameEdit.Text, pNumEdit.Text, emailEdit.Text };
                DBC.Add(arr);
                DBC.Delete(id);
                contact = DBC.getData();
                ((MainWindow)Application.Current.MainWindow).myDataGrid.ItemsSource = null;
                ((MainWindow)Application.Current.MainWindow).myDataGrid.ItemsSource = contact;
                MainWindow.isAvailable = true;
                Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.isAvailable = true;
        }
    }
}
