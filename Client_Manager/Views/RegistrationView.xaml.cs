using Client_Manager.Models;
using Client_Manager.Views;
using ModelProvider;
using ModelProvider.Models;
using ModelProvider.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Automation;
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
        private RepairView repair;
        private bool update;

        public RegistrationView(bool update = false)
        {
            InitializeComponent();
            this.update = update;
            this.repair = ManagerService.GetInstance().Repair;
            if (update) LoadRepair(repair);
            managerLbl.Content = ManagerService.GetInstance().CurrentManager?.User.Username ?? "adminInside%";
        }

        private void LoadRepair(ModelProvider.ViewModels.RepairView repair)
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

            var regValidator = new RegistrationFormValidator(update);

            if (regValidator.ValidateClientInput(new List<TextBox> { clientNameTblock, clientPhoneTblock, clientEmailTblock })
                &&
                regValidator.ValidateAutoInput(new List<TextBox> { autoBrandTblock, autoModelTblock, autoPlateTblock }))
            {

                if (!update)
                    AssembleNewRepairRecord();
                else
                    UpdateRepairRecord();                

                DialogResult = true;
                Hide();
                if (new RepairJobView(update).ShowDialog() == true)
                    Close();
            }


        }
        public void UpdateRepairRecord()
        {
            repair.Auto.Client.Name = clientNameTblock.Text;
            repair.Auto.Client.Email = clientEmailTblock.Text;
            repair.Auto.Client.PhoneNumber = clientPhoneTblock.Text;

            repair.Auto.Brand = autoBrandTblock.Text;
            repair.Auto.Model = autoModelTblock.Text;
            repair.Auto.LicencePlate = autoPlateTblock.Text;
         
        }
        public void AssembleNewRepairRecord()
        {
            ClientView newClient = new ClientView();
            newClient.Name = clientNameTblock.Text;
            newClient.Email = clientEmailTblock.Text;
            newClient.PhoneNumber = clientPhoneTblock.Text;

            AutoView auto = new AutoView();
            auto.Brand = autoBrandTblock.Text;
            auto.Model = autoModelTblock.Text;
            auto.LicencePlate = autoPlateTblock.Text;
            auto.Client = newClient;
            
            repair.Auto = auto;
            repair.State = State.New;
            repair.Price = 0;
            repair.BonusRepairs = new List<BonusRepairView>();
            repair.Manager ??= ManagerService.GetInstance().CurrentManager;
            repair.Description = "";
            repair.RepairTechnicians = new List<RepairTechnicianView>();

        }
    }
}
