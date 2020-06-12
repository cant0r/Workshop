using Client_Manager.CustomControls;
using Client_Manager.Models;
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
    /// Interaction logic for RepairLogView.xaml
    /// </summary>
    public partial class RepairLogView : Window
    {
        public RepairLogView(long repairID)
        {
            InitializeComponent();
            LoadRepairLogs(repairID);
        }

        private void LoadRepairLogs(long repairID)
        {
            var repairLogs = ManagerService.GetInstance().RepairLogs.FindAll(l => l.Repair.Id == repairID);
            
            foreach(RepairLog log in repairLogs)
            {
                var logEntry = new RepairLogEntry();
                logEntry.techIDLbl.Content = log.TechnicianId.ToString();
                logEntry.logTblock.Text = log.Description.ToString();
                logEntry.dobLbl.Content = log.Date;
                logEntryPanel.Children.Add(logEntry);
            }
            repairJobIDlbl.Content = repairID.ToString();         
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }
    }
}
