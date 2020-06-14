using Client_Technician.CustomControls;
using Client_Technician.Models;
using ModelProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client_Technician.Views
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        private TechnicianService technicianService;
        public Overview()
        {
            InitializeComponent();
            technicianService = TechnicianService.GetInstance();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }
        private void LoadRepairs(Predicate<Repair> predicate)
        {
            var myRepairs = TechnicianService.GetInstance().Repairs.FindAll(predicate);

            foreach (Repair r in myRepairs)
            {
                var repairEntry = new RepairJobEntry();
                repairEntry.descriptionTblock.Text = r.Description;
                repairEntry.repairIdLbl.Content = "ID: " + r.Id.ToString();
                repairEntry.repairStateLbl.Content = r.State;
                repairEntry.licencePlateLbl.Content = r.Auto.LicencePlate.ToString();
                repairEntry.managerLbl.Content = r.Manager?.User?.Username ?? "adminInside%";

                List<long> techIDs = new List<long>();
                List<Technician> techs = new List<Technician>();
                if (r.RepairTechnicians != null)
                    techIDs = (from repair in r.RepairTechnicians
                               where repair.RepairID == r.Id
                               select repair.TechnicianId).ToList();
                if (technicianService.Technicians != null)
                    techs = technicianService.Technicians.FindAll(t => techIDs.Contains(t.Id));
                foreach (Technician t in techs)
                {
                    repairEntry.techsLbox.Items.Add(t.Name);
                }

                repairEntry.doneBtn.Click += (object sender, RoutedEventArgs args) =>
                {
                    r.State = State.Done;
                    if (MessageBox.Show("Leadás után a munka automatikusan írásvédett lesz.", "Biztosan leadja a munkát?", MessageBoxButton.YesNo)
                     == MessageBoxResult.Yes)
                        repairEntry.Visibility = Visibility.Collapsed;
                };


                entryPanel.Children.Add(repairEntry);

            }
        }
    }
}
