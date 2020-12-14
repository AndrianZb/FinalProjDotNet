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
    /// Interaction logic for PopUpAdd.xaml
    /// </summary>
    public partial class PopUpAdd : Window
    {
        List<ContactsCreator> contact = new List<ContactsCreator>();
        DBConnection DBC = DBConnection.instance;
        public PopUpAdd()
        {
            InitializeComponent();
        }

       
            private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Regex phoneCheck = new Regex(@"^\d{10}$");
            Regex emailCheck = new Regex(@"^[^@\s]+@[^@\s\.]+\.[^@\.\s]+$");

            if (fNameAdd.Text.Length == 0)
            {
                MessageBox.Show("First name field cannot be empty!", "Alert");
            }
            else if (fNameAdd.Text.Length >= 50)
            {
                MessageBox.Show("First name field can only be 50 Characters maximum!", "Alert");
            }

            else if (lNameAdd.Text.Length == 0)
            {
                MessageBox.Show("Last name field cannot be empty!", "Alert");
            }
            else if (lNameAdd.Text.Length >= 50)
            {
                MessageBox.Show("Last name field can only be 50 Characters maximum!", "Alert");
            }

            else if (pNumAdd.Text.Length == 0)
            {
                MessageBox.Show("Phone number field cannot be empty!", "Alert");
            }
            else if (!phoneCheck.IsMatch(pNumAdd.Text))
            {
                MessageBox.Show("Phone number can only be 10 numbers maximum example: '1112223333'!", "Alert");
            }

            else if (emailAdd.Text.Length == 0)
            {
                MessageBox.Show("Email field cannot be empty!", "Alert");
            }
            else if (emailAdd.Text.Length >= 150)
            {
                MessageBox.Show("Email field can be 50 Characters maximum!", "Alert");
            }
            
            else if (!emailCheck.IsMatch(emailAdd.Text))
            {
                MessageBox.Show("Email has to be valid example: 'name@domain.com'!", "Alert");
            }
            else
            {
                string[] arr = { fNameAdd.Text, lNameAdd.Text, pNumAdd.Text, emailAdd.Text };
                DBC.Add(arr);

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
