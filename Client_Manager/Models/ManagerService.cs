using ModelProvider;
using ModelProvider.ViewModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows.Automation;

namespace Client_Manager.Models
{
    public sealed class ManagerService : IManager
    {
        public List<AutoView> Autos { get; private set; }
        public List<ClientView> Clients { get; private set; }
        public List<RepairView> Repairs { get; private set; }
        public List<RepairLogView> RepairLogs { get; private set; }
        public List<TechnicianView> Technicians { get; private set; }
        public List<ManagerView> Managers { get; private set; }
        public List<BonusRepairView> BonusRepairs { get; private set; }
        public List<BonusView> Bonuses { get; private set; }
        public ManagerView CurrentManager { get; set; }
        public RepairView Repair { get; set; }

        private WorkshopClient workshopClient;

        private static readonly ManagerService instance;


        static ManagerService()
        {
            instance = new ManagerService();
        }
        private ManagerService()
        {
            workshopClient = WorkshopClient.GetInstance();

        }
        public static ManagerService GetInstance()
        {
            instance?.ParseDatabase();
            return instance;
        }

        public void ParseDatabase()
        {
            Autos = workshopClient.RetrieveEntities<AutoView>() ?? new List<AutoView>();
            Clients = workshopClient.RetrieveEntities<ClientView>() ?? new List<ClientView>();
            Repairs = workshopClient.RetrieveEntities<RepairView>() ?? new List<RepairView>();
            RepairLogs = workshopClient.RetrieveEntities<RepairLogView>() ?? new List<RepairLogView>();
            Technicians = workshopClient.RetrieveEntities<TechnicianView>() ?? new List<TechnicianView>();
            Managers = workshopClient.RetrieveEntities<ManagerView>() ?? new List<ManagerView>();
            Bonuses = workshopClient.RetrieveEntities<BonusView>() ?? new List<BonusView>();
            BonusRepairs = workshopClient.RetrieveEntities<BonusRepairView>() ?? new List<BonusRepairView>();
        }

        public bool UploadRepair(RepairView r)
        {
            return workshopClient.UploadRepair(r);
        }
        public bool UploadUpdatedRepair(RepairView r)
        {
           return  workshopClient.UploadUpdatedRepair(r);
        }
        public bool ValidateUser(UserView u)
        {
            var valid = workshopClient.ValidateUser(u);
            ManagerView manager = 
                (from managers in Managers ?? new List<ManagerView>()
                 where managers.User.Username == u.Username 
                 select managers).FirstOrDefault(); 
            
            CurrentManager = manager;
            Repair = new RepairView();
            return valid && manager != null;
        }

        public bool ValidateLicencePlate(string a)
        {
            var valid = workshopClient.ValidateLicencePlate(a);
            return valid;
        }
        public bool ValidateClientEmail(string m)
        {
            var valid = workshopClient.ValidateClientEmail(m);
            return valid;
        }

    }
}
