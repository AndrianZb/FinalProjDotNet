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
            string[] arr = {fNameAdd.Text, lNameAdd.Text, pNumAdd.Text, emailAdd.Text };
            DBC.Add(arr);

            contact = DBC.getData();
            ((MainWindow)Application.Current.MainWindow).myDataGrid.ItemsSource = null;
            ((MainWindow)Application.Current.MainWindow).myDataGrid.ItemsSource = contact;
            this.Close();
        }
    }
}
