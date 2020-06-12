﻿using Client_Manager.CustomControls;
using Client_Manager.Models;
using ModelProvider;
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
using System.Windows.Threading;

namespace Client_Manager
{
    /// <summary>
    /// Interaction logic for Database.xaml
    /// </summary>
    public partial class DatabaseView : Window
    {
        private Button activeButton;

        private ManagerService theManager;

        public DatabaseView()
        {
            InitializeComponent();
            theManager = ManagerService.GetInstance();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (object sender, EventArgs args) =>
            {
                entryPanel.Children.Clear();
                theManager.ParseDatabase();
                
            };
            timer.Interval = TimeSpan.FromMinutes(5);
            timer.Start();
        }

        private void MakeButtonActive(Button btn)
        {
            FlipColorsOfButton(activeButton);
            activeButton = btn;
            FlipColorsOfButton(activeButton);
        }

        private void FlipColorsOfButton(Button btn)
        {
            if (btn is null)
                return;
            Brush temp = btn.Background;
            btn.Background = btn.Foreground;
            btn.Foreground = temp;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = true;
        }

        private void progressBtn_Click(object sender, RoutedEventArgs e)
        {
            //Justification for the Button casting: Only a single Button instance's click event is tied to this method.
            MakeButtonActive((Button)sender);

            foreach(Repair r in theManager.Repairs)
            {
                var repairEntry = new RepairEntryBox();
                repairEntry.descriptionTblock.Text = r.Description;
                repairEntry.repairIdLbl.Content = r.Id.ToString();
                repairEntry.repairStateLbl.Content = r.State;

                var techIDs = (from repair in r.RepairTechnicians
                               where repair.RepairID == r.Id
                               select repair.TechnicianId);

                var techs = theManager.Technicians.FindAll(t => techIDs.Contains(t.Id));
                foreach (Technician t in techs)
                {
                    repairEntry.techsLbox.Items.Add(t.Name);
                }

                repairEntry.repairLogBtn.Click += (object sender, RoutedEventArgs args) => 
                {
                    new RepairLogView(r.Id).ShowDialog();
                };

                repairEntry.editBtn.Click += (object sender, RoutedEventArgs args) =>
                {

                };

                entryPanel.Children.Add(repairEntry);

            }
            

        }

        private void closedBtn_Click(object sender, RoutedEventArgs e)
        {          
            MakeButtonActive((Button)sender);
        }

        private void techniciansBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeButtonActive((Button)sender);
        }

        private void clientsBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeButtonActive((Button)sender);
        }

        private void autosBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeButtonActive((Button)sender);
        }
    }
}
