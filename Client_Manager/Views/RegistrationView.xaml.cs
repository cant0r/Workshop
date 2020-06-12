using Client_Manager.Views;
using ModelProvider;
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

namespace Client_Manager
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        private Repair repair = null;

        public RegistrationView(Repair repair=null)
        {
            InitializeComponent();
            this.repair = repair;
            if(!(this.repair is null)) LoadRepair(this.repair);
        }

        private void LoadRepair(Repair repair)
        {
            if (repair is null)
            {
                MessageBox.Show("NULL was received as argument", "The given Repair type argument is null", MessageBoxButton.OK);
                Close();
            }
            var auto = repair.Auto;

            autoBrandTblock.Text = auto.Brand.ToString();
            autoModelTblock.Text = auto.Model.ToString();
            autoPlateTblock.Text = auto.LicencePlate.ToString();

            clientNameTblock.Text = auto.Client.Name.ToString();
            clientEmailTblock.Text = auto.Client.Email.ToString();
            clientPhoneTblock.Text = auto.Client.PhoneNumber.ToString();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Hide();
            if (new RepairView(repair).ShowDialog() == true)
                Close();
        }
    }
}
