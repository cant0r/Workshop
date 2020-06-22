using Client_Manager.Models;
using ModelProvider.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client_Manager.Tests
{
    public class MockManager : IManager
    {
        public bool Result { get; set; } = false;
        public void ParseDatabase()
        {
            ;
        }

        public bool UploadRepair(RepairView r)
        {
            return Result;
        }

        public bool UploadUpdatedRepair(RepairView r)
        {
            return Result;
        }

        public bool ValidateClientEmail(string m)
        {
            return Result;
        }

        public bool ValidateLicencePlate(string a)
        {
            return Result;
        }

        public bool ValidateUser(UserView u)
        {
            return Result;
        }
    }
}
