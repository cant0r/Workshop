using Client_Manager.Models;
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

namespace Client_Manager.Views
{
    /// <summary>
    /// Interaction logic for RepairView.xaml
    /// </summary>
    public partial class RepairView : Window
    {
        public RepairView(Repair repair = null)
        {
            InitializeComponent();
            LoadBonuses();
            if (!(repair is null))
            {                
                LoadRepair(repair);
            } 
        }

        private void LoadRepair(Repair repair)
        {
            realPriceLbl.Content = repair.Price.ToString();
            problemTbox.Text = repair.Description.ToString();

            long noBonusesPrice = repair.Price - repair.Bonuses.Sum(b => b.Price);
            priceTbox.Text = noBonusesPrice.ToString();

            foreach(Bonus b in repair.Bonuses)
            {
                Label name = new Label();
                name.Content = b.Name;
                Label price = new Label();
                price.Content = b.Price;

                bonusStackPanel.Children.Add(name);
                bonusPriceStackPanel.Children.Add(price);

               
                foreach(CheckBox cb in bonusLbox.Children)
                {
                    if(cb.Name == b.Name)
                    {
                        cb.IsChecked = true;
                        break;
                    }
                }
            }
            
        }
        private void LoadBonuses()
        {
            var bonuses = WorkshopClient.GetInstance().RetrieveEntities<Bonus>();

            foreach(Bonus b in bonuses)
            {
                CheckBox cbox = new CheckBox();
                cbox.Content = b.Name;
                bonusLbox.Children.Add(cbox);
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
