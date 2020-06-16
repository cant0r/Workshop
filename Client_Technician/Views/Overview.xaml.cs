using Client_Technician.CustomControls;
using Client_Technician.Models;
using ModelProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Client_Technician.Views
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        private TechnicianService technicianService;
        public Overview(bool jobBoard = false)
        {
            InitializeComponent();
            technicianService = TechnicianService.GetInstance();
            if (jobBoard)
                LoadAvailableRepairs();
            else
                LoadRepairs();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }
        private void LoadRepairs()
        {
            var myRepairs = technicianService.GetRepairsByTechnicianId(technicianService.CurrentTechnician);
            myRepairs = from reps in myRepairs where reps.State == State.InProgress select reps;

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
                repairEntry.repairLogBtn.Click += (object sender, RoutedEventArgs args) =>
                {
                    
                    topbarpanel.Children.Clear();
                    entryPanel.Children.Clear();
                    LoadRepairLogs(r.Id);
                };
                repairEntry.doneBtn.Click += (object sender, RoutedEventArgs args) =>
                {
                    r.State = State.Done;
                    if (MessageBox.Show("Leadás után a munka automatikusan írásvédett lesz.", "Biztosan leadja a munkát?", MessageBoxButton.YesNo)
                     == MessageBoxResult.Yes)
                        repairEntry.Visibility = Visibility.Collapsed;
                    r.Manager?.Repair?.Clear();
                    technicianService.UpdateRepair(r);
                };

                
                entryPanel.Children.Add(repairEntry);

            }
        }
        private void LoadRepairLogs(long repairID)
        {
            
            technicianService.ParseDatabase();
            var repairLogs = technicianService.RepairLogs.FindAll(l => l.Repair.Id == repairID);
            var topbar = new RepairLogEditorTopbar();

            topbar.newBtn.Click += (object sender, RoutedEventArgs args) =>
            {
                var logEntry = new RepairLogEntry();
                logEntry.techIDLbl.Content = technicianService.CurrentTechnician.Id;
                logEntry.dobLbl.Content = DateTime.Now.ToShortDateString();
                entryPanel.Children.Insert(0,logEntry);
            };

            topbar.backBtn.Click += (object sender, RoutedEventArgs args) =>
            {
                topbarpanel.Children.Clear();
                entryPanel.Children.Clear();
                LoadRepairs();
            };

            topbar.saveBtn.Click += (object sender, RoutedEventArgs args) =>
            {               
                foreach(UIElement uiElement in entryPanel.Children)
                {
                    if(uiElement is RepairLogEntry)
                    {
                        var rle = (RepairLogEntry)uiElement;
                        if (!rle.Modified)
                            continue;
                        var rlog = new RepairLog();
                        rlog.Date = DateTime.Parse(rle.dobLbl.Content.ToString());
                        rlog.TechnicianId = Convert.ToInt64(rle.techIDLbl.Content.ToString());
                        rlog.Description = rle.logTblock.Text;
                        rlog.Id = rle.LogId;
                        rlog.Repair = technicianService.Repairs.FirstOrDefault(r => r.Id == repairID);
                        rlog.Repair?.Manager?.Repair?.Clear();
                        technicianService.UploadRepairLog(rlog);
                    }
                }

            };
            topbarpanel.Children.Add(topbar);

            foreach (RepairLog log in repairLogs)
            {
                var logEntry = new RepairLogEntry();
                logEntry.techIDLbl.Content = log.TechnicianId.ToString();
                logEntry.logTblock.Text = log.Description.ToString();              
                logEntry.dobLbl.Content = log.Date;
                logEntry.LogId = log.Id;
                entryPanel.Children.Insert(0,logEntry);                
            }
            topbar.repairIdLbl.Content = repairID.ToString();
        }
        private void LoadAvailableRepairs()
        {
            var myRepairs = technicianService.GetAvailableRepairs();

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
                    r.State = State.InProgress;
                    r.Manager.Repair.Clear();
                    r.RepairTechnicians.Add(
                        new RepairTechnician
                        {
                            RepairID = r.Id,
                            TechnicianId = technicianService.CurrentTechnician.Id
                        }
                    );
                    technicianService.UpdateRepair(r);
                    repairEntry.Visibility = Visibility.Collapsed;
                };
                repairEntry.doneBtn.Content = "Felvesz";
                repairEntry.repairLogBtn.IsEnabled = false;

               

                entryPanel.Children.Add(repairEntry);

            }
        }
    }
}
