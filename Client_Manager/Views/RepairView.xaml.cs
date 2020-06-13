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
                Label bonus = new Label();
                bonus.Content = b.Name + " " + b.Price;
                bonus.HorizontalContentAlignment = HorizontalAlignment.Center;
                bonus.VerticalContentAlignment = VerticalAlignment.Center;
                bonusStackPanel.Children.Add(bonus);

               
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
                cbox.Checked += (object sender, RoutedEventArgs args) => 
                {
                    Label name = new Label();
                    name.Content = b.Name + " " + b.Price;
                    AddOrRemoveBonus(name);
                };
                cbox.Unchecked += (object sender, RoutedEventArgs args) =>
                {
                    Label name = new Label();
                    name.Content = b.Name + " "  + b.Price;
                    AddOrRemoveBonus(name);
                };
                bonusLbox.Children.Add(cbox);
            }

        }

      
        private void AddOrRemoveBonus(Label name)
        {

            Action<Label,StackPanel> addOrRemove = (Label c, StackPanel sp) => 
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
            };

            addOrRemove(name, bonusStackPanel);
            
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
