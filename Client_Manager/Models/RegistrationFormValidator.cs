using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace Client_Manager.Models
{
    public class RegistrationFormValidator
    {
        private IManager manager;
        private bool Update { get; set; }

        public RegistrationFormValidator(bool update)
        {
            Update = update;
            manager = ManagerService.GetInstance();
        }
        public RegistrationFormValidator(bool update, IManager service)
        {
            Update = update;
            manager = service;
        }
        public bool ValidateClientInput(List<TextBox> controls)
        {
            Regex fullNameRegex = new Regex(@"[A-Za-z]+ [A-Za-z]+( [A-Za-z]+)*", RegexOptions.Compiled);
            Regex phoneNumber = new Regex(@"[0-9]{11}.*", RegexOptions.Compiled);

            var fullNameValid = fullNameRegex.IsMatch(controls[0]?.Text);

            if (!fullNameValid)
                controls[0].Foreground = Brushes.Red;
            else if (controls[0].Foreground == Brushes.Red)
                controls[0].Foreground = new TextBox().Foreground;


            var phoneNumberValid = phoneNumber.IsMatch(controls[1]?.Text);

            if (!phoneNumberValid)           
                controls[1].Foreground = Brushes.Red;
            else if (controls[1].Foreground == Brushes.Red)
                controls[1].Foreground = new TextBox().Foreground;

            var emailValid = true;

            try
            {
                if (controls[2]?.Text == "" || (!Update && !manager.ValidateClientEmail(controls[2]?.Text)) ||
                    !(controls[2]?.Text.ToString() == new MailAddress(controls[2]?.Text.ToString()).Address))
                    emailValid = false;

            }
            catch (FormatException e)
            {
                Console.Error.WriteLine(e.Message);
                emailValid = false;
            }
           
            if (!emailValid)            
                controls[2].Foreground = Brushes.Red;
            else if (controls[2].Foreground == Brushes.Red)
                controls[2].Foreground = new TextBox().Foreground;

            return fullNameValid & phoneNumberValid & emailValid;

        }
        public bool ValidateAutoInput(List<TextBox> controls)
        {
            var result = true;

            foreach(TextBox t in controls)
            {
                if(t.Text.Length == 0)
                {
                    result = false;
                    t.Foreground = Brushes.Red;
                }
                else if(t.Foreground == Brushes.Red)
                {
                    t.Foreground = new TextBox().Foreground;
                }
            }
            var valid = Regex.IsMatch(controls[2]?.Text, @"[A-Z0-9]+([\s\S\-]*[A-Z0-9]*)*");
            if(valid)
                valid = Update || manager.ValidateLicencePlate(controls[2]?.Text);
            if(!valid)
            {
                controls[2].Foreground = Brushes.Red;
            }
            return result & valid;
        }

        public bool ValidateCostValue(TextBox t)
        {
            if (t.Text == "" || !(Regex.IsMatch(t.Text, @"[0-9]+")))
                return false;
            return true;

        }
    }
}
