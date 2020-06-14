using Client_Manager.Models;
using ModelProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client_Manager.Views
{
    /// <summary>
    /// Interaction logic for RepairView.xaml
    /// </summary>
    public partial class RepairView : Window
    {

        private Repair theRepair;
        private IEnumerable<Bonus> bonuses = null;
        private bool update = false;

        public RepairView(Repair repair = null)
        {
            InitializeComponent();
            LoadBonuses();
            if (!(repair is null))
            {
                update = true;
                LoadRepair(repair);
            }
        }

        private void LoadRepair(Repair repair)
        {
            theRepair = repair;
            realPriceLbl.Content = theRepair.Price;
            problemTbox.Text = theRepair.Description.ToString();

            long noBonusesPrice = theRepair.Price - theRepair?.Bonuses.Sum(b => b.Price) ?? 0;
            priceTbox.Text = noBonusesPrice.ToString();

            foreach (Bonus b in theRepair.Bonuses)
            {
                Label bonus = new Label();
                bonus.Content = b.Name + " " + b.Price;
                bonus.HorizontalContentAlignment = HorizontalAlignment.Center;
                bonus.VerticalContentAlignment = VerticalAlignment.Center;
        
                foreach (CheckBox cb in bonusLbox.Children)
                {
                    if (cb.Content.ToString().Contains(b.Name))
                    {
                        cb.IsChecked = true;
                        break;
                    }
                }
            }

        }
        private void LoadBonuses()
        {
            bonuses = WorkshopClient.GetInstance().RetrieveEntities<Bonus>();

            foreach (Bonus b in bonuses)
            {
                CheckBox cbox = new CheckBox();
                cbox.Content = b.Name;
                cbox.Checked += (object sender, RoutedEventArgs args) =>
                {
                    Label name = new Label();
                    name.Content = b.Name + " " + b.Price;
                    AddBonus(name);
                    RefreshClientBonuses();
                    UpdateRealPrice();
                };
                cbox.Unchecked += (object sender, RoutedEventArgs args) =>
                {
                    Label name = new Label();
                    name.Content = b.Name + " " + b.Price;
                    RemoveBonus(name);
                    RefreshClientBonuses();
                    UpdateRealPrice();
                };
                bonusLbox.Children.Add(cbox);
            }

        }

        private void RefreshClientBonuses()
        {
            List<Bonus> selectedBonuses = new List<Bonus>();

            foreach (Label bl in bonusStackPanel.Children)
            {
                foreach (Bonus b in bonuses)
                {
                    if (bl.Content.ToString().Contains(b.Name))
                    {
                        selectedBonuses.Add(b);
                        break;
                    }
                }
            }

            theRepair.Bonuses = selectedBonuses;
        }

        private void AddBonus(Label name)
        {
            bonusStackPanel.Children.Add(name);
        }
        private void RemoveBonus(Label name)
        {
            var b_temp = (from ctrl in bonusStackPanel.Children.OfType<UIElement>()
                          where ((Label)ctrl).Content.ToString().Contains(name.Content.ToString())
                          select ctrl).FirstOrDefault();

            bonusStackPanel.Children.Remove(b_temp);
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {

            theRepair.Description = problemTbox.Text;
            theRepair.Price = (long)realPriceLbl.Content;
            if (update)
                ManagerService.GetInstance().UploadUpdatedRepair(theRepair);
            else
                ManagerService.GetInstance().UploadRepair(theRepair);

            ManagerService.GetInstance().ParseDatabase();

            DialogResult = true;
            Close();
        }
        private void UpdateRealPrice()
        {
            if (new RegistrationFormValidator().ValidateCostValue(priceTbox))
                realPriceLbl.Content = long.Parse(Regex.Replace(priceTbox.Text, @"\s", "")) + (theRepair?.Bonuses.Sum(b => b.Price) ?? 0);
        }

        private void priceTbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRealPrice();
        }
    }
}
