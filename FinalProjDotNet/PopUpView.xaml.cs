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
    /// Interaction logic for PopUpView.xaml
    /// </summary>
    public partial class PopUpView : Window
    {
        public PopUpView()
        {
            InitializeComponent();
        }

        private void ViewClose_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.isAvailable = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.isAvailable = true;
        }
    }
}
