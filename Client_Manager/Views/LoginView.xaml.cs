using Client_Manager.Models;
using ModelProvider;
using ModelProvider.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Client_Manager.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private bool validated = false;
        public LoginView()
        {
            InitializeComponent();
            DispatcherTimer clock = new DispatcherTimer();
            clock.Interval = TimeSpan.FromSeconds(1);
            clock.Tick += (object sender, EventArgs args) =>
            {
                clockLbl.Content = DateTime.Now.ToLongDateString();
            };
            clock.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!validated)
                Application.Current.Shutdown(-1337);
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            UserView u = new UserView();
            u.Username = usernameTbox.Text.ToString();
            u.Password = passwordBox.Password.ToString();
            u.isManager = true;
            u.Email = null;

            if (ManagerService.GetInstance().ValidateUser(u))
            {               
                validated = true;
                DialogResult = true;
                Close();
            }            

        }
    }
}
