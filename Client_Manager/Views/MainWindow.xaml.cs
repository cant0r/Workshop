using Client_Manager.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client_Manager
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

        private void databaseBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            if (new DatabaseView().ShowDialog() == true)
                Show();
              
         
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WorkshopClient.GetInstance().Dispose();
            Application.Current.Shutdown(0);
        }

        private void newrepairBtn_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            if (new RegistrationView().ShowDialog() == true)
                Show();
        }
    }
}
