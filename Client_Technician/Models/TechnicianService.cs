using ModelProvider;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Automation;

namespace Client_Technician.Models
{
    public sealed class TechnicianService
    {
        public List<Repair> Repairs { get; private set; }
        public List<Technician> Technicians { get; private set; }

        public Technician CurrentTechnician { get; set; }

        private WorkshopClient workshopClient;

        private static readonly TechnicianService instance;


        static TechnicianService()
        {
            instance = new TechnicianService();
        }
        private TechnicianService()
        {
            workshopClient = WorkshopClient.GetInstance();

        }
        public static TechnicianService GetInstance()
        {
            instance?.ParseDatabase();
            return instance;
        }

        public void ParseDatabase()
        {          
            Repairs = workshopClient.RetrieveEntities<Repair>() ?? new List<Repair>();
            Technicians = workshopClient.RetrieveEntities<Technician>() ?? new List<Technician>();
        }

        public void UpdateRepair(Repair r)
        {
            workshopClient.UpdateRepair(r);
        }
        public bool ValidateUser(User u)
        {
            var valid = workshopClient.ValidateUser(u);
            Technician tech = 
                (from techs in Technicians?.OfType<Technician>() ?? new List<Technician>()
                 where techs.User.Username == u.Username
                 select techs).FirstOrDefault();
           
            CurrentTechnician = tech;
            return valid;
        }

        public IEnumerable<Repair> GetRepairsByTechnicianId(Technician t)
        {
            return workshopClient.GetRepairsByTechnicianId(t);
        }

    }
}
