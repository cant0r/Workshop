﻿using Client_Technician.Models;
using ModelProvider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

namespace Client_Technician.Views
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
            User u = new User();
            u.Username = usernameTbox.Text.ToString();
            u.Password = passwordBox.Password.ToString();
            u.isManager = false;

            if (TechnicianService.GetInstance().ValidateUser(u))
            {
                validated = true;
                DialogResult = true;
                Close();
            }
            
        }
    }
}