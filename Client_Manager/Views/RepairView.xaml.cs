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
            //ITT BECRASHELT VALAHOGY
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
                bonusStackPanel.Children.Add(bonus);


                foreach (CheckBox cb in bonusLbox.Children)
                {
                    if (cb.Name == b.Name)
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
                    AddOrRemoveBonus(name);
                    CalculateActualPrice();
                };
                cbox.Unchecked += (object sender, RoutedEventArgs args) =>
                {
                    Label name = new Label();
                    name.Content = b.Name + " " + b.Price;
                    AddOrRemoveBonus(name);
                    CalculateActualPrice();
                };
                bonusLbox.Children.Add(cbox);
            }

        }

        private void CalculateActualPrice()
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

        private void AddOrRemoveBonus(Label name)
        {

            Action<Label, StackPanel> addOrRemove = (Label c, StackPanel sp) =>
             {
                 var b_temp = (from ctrl in sp.Children.OfType<UIElement>()
                               where ((Label)ctrl).Content.ToString().Contains(c.Content.ToString())
                               select ctrl).FirstOrDefault();
                 if (b_temp != null)
                 {
                     sp.Children.Remove(b_temp);
                 }
                 else
                 {
                     sp.Children.Add(c);
                 }
                 //Triggering the textchanged event of priceTbox
                 priceTbox.Text = priceTbox.Text;
             };

            addOrRemove(name, bonusStackPanel);

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

        private void priceTbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (new RegistrationFormValidator().ValidateCostValue(priceTbox))
                realPriceLbl.Content = long.Parse(Regex.Replace(priceTbox.Text, @"\s", "")) + (theRepair?.Bonuses.Sum(b => b.Price) ?? 0);
        }
    }
}
