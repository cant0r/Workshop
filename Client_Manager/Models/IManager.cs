using ModelProvider.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client_Manager.Models
{
    public interface IManager
    {
        public void ParseDatabase();

        public bool UploadRepair(RepairView r);
        public bool UploadUpdatedRepair(RepairView r);
        public bool ValidateUser(UserView u);

        public bool ValidateLicencePlate(string a);
        public bool ValidateClientEmail(string m);

    }
}
