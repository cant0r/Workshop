﻿using Client_Manager.Models;
using ModelProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public RepairView(bool update = false)
        {
            InitializeComponent();           
            theRepair = ManagerService.GetInstance().Repair;
            LoadBonuses();
            LoadRepair(theRepair);
            
            this.update = update;
            
            managerLbl.Content = (theRepair?.Manager?.User.Username ?? ManagerService.GetInstance().CurrentManager?.User.Username) ?? "adminInside%";
            priceTbox.MaxLength = Int64.MaxValue.ToString().Length;
        }

        private void LoadRepair(Repair repair)
        {
            theRepair = repair;
            realPriceLbl.Content = theRepair.Price;
            problemTbox.Text = theRepair.Description.ToString();
            var bns = ManagerService.GetInstance().BonusRepairs.Where(br => br.RepairID == repair.Id).Select(br => br.Bonus).ToList();
            long noBonusesPrice = theRepair.Price - bns.Sum(b => b?.Price ?? 0);
 
            priceTbox.Text = noBonusesPrice.ToString();

            foreach (Bonus b in bns)
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
            bonuses = ManagerService.GetInstance().Bonuses;
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
            theRepair.BonusRepairs = new List<BonusRepair>();
            foreach(Bonus b in selectedBonuses)
            {                
                theRepair.BonusRepairs.Add(new BonusRepair
                {
                    Repair = theRepair,
                    Bonus = b,
                    RepairID = theRepair.Id,
                    BonusName = b.Name
                });
            }

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
            var repBonusCost = theRepair.BonusRepairs?.Select(br => br?.Bonus)?.Sum(b => b?.Price) ?? 0;

            if (new RegistrationFormValidator().ValidateCostValue(priceTbox))
            {
                try
                {
                    realPriceLbl.Content = Math.Abs(Convert.ToInt64(Regex.Replace(priceTbox.Text, @"\s", ""))) + repBonusCost;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.ToString());
                    realPriceLbl.Content = repBonusCost;
                }
            }
        }

        private void priceTbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRealPrice();
        }
    }
}
