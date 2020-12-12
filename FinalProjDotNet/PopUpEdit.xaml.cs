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
            int id = int.Parse(idEdit.Text);

            string[] arr = { fNameEdit.Text, lNameEdit.Text, pNumEdit.Text, emailEdit.Text };
            DBC.Add(arr);
            DBC.Delete(id);
            contact = DBC.getData();
            ((MainWindow)Application.Current.MainWindow).myDataGrid.ItemsSource = null;
            ((MainWindow)Application.Current.MainWindow).myDataGrid.ItemsSource = contact;
            this.Close();
        }
    }
}
