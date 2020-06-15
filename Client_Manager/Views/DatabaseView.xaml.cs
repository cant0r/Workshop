using Client_Manager.CustomControls;
using Client_Manager.Models;
using ModelProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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

        private ManagerService manegerService;

        private object dataGridSource;

        public DatabaseView()
        {
            InitializeComponent();
            manegerService = ManagerService.GetInstance();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += (object sender, EventArgs args) =>
            {
                entryPanel.Children.Clear();
                manegerService.ParseDatabase();

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
        private void LoadRepairsViaPredicate(Func<Repair, bool> predicate)
        {
            entryPanel.Children.Clear();
            foreach (Repair r in manegerService.Repairs.FindAll(r => predicate(r)))
            {
                var repairEntry = new RepairEntryBox();
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
                if (manegerService.Technicians != null)
                    techs = manegerService.Technicians.FindAll(t => techIDs.Contains(t.Id));
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
                    manegerService.Repair = r;
                    new RegistrationView(true).ShowDialog();
                };

                if (r.State == State.Cancelled || r.State == State.Done)
                {
                    repairEntry.deleteBtn.Visibility = Visibility.Collapsed;
                    repairEntry.editBtn.Visibility = Visibility.Collapsed;
                    ((UIElement)repairEntry.deleteBtn.Parent).Visibility = Visibility.Collapsed;
                }

                repairEntry.deleteBtn.Click += (object sender, RoutedEventArgs args) =>
                {
                    if(MessageBox.Show("A lezárt szereléseket nem lehet a jövőben folytatni.", "Megerősítés szüksgées", MessageBoxButton.OKCancel)
                     == MessageBoxResult.OK)
                    {
                        r.State = State.Cancelled;
                        repairEntry.Visibility = Visibility.Collapsed;
                        manegerService.UploadUpdatedRepair(r);
                    }
                };

                entryPanel.Children.Add(repairEntry);

            }
        }
        private void LoadDataIntoGrid<TEntity>(IEnumerable<TEntity> data)
        {
            entryPanel.Children.Clear();
            dataGridSource = data;
            DataGrid dataTable = new DataGrid();
            dataTable.ItemsSource = data;
            dataTable.IsReadOnly = true;

            TextBox searchTbox = new TextBox();

            searchTbox.TextChanged += (object sender, TextChangedEventArgs args) =>
            {
                if (searchTbox.Text.Length == 0)
                    dataTable.ItemsSource = (System.Collections.IEnumerable)dataGridSource;
                else
                    dataTable.ItemsSource =
                        dataTable.ItemsSource.OfType<TEntity>().Where(i => i.ToString().Contains(searchTbox.Text));
            };

            entryPanel.Children.Add(searchTbox);
            entryPanel.Children.Add(dataTable);
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
            LoadRepairsViaPredicate(
                (Repair r) => { return r.State == State.InProgress || r.State == State.New; });
        }

        private void closedBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeButtonActive((Button)sender);
            LoadRepairsViaPredicate(
                (Repair r) => { return r.State == State.Cancelled || r.State == State.Done; });
        }

        private void techniciansBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeButtonActive((Button)sender);
            LoadDataIntoGrid(manegerService.Technicians);
        }

        private void clientsBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeButtonActive((Button)sender);
            LoadDataIntoGrid(manegerService.Clients);
        }

        private void autosBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeButtonActive((Button)sender);
            LoadDataIntoGrid(manegerService.Autos);
        }
    }
}
