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
        public List<RepairLog> RepairLogs { get; private set; }


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
            RepairLogs = workshopClient.RetrieveEntities<RepairLog>() ?? new List<RepairLog>();
        }

     
        public void UpdateRepair(Repair r)
        {
            var newTechnician = r.RepairTechnicians?.FirstOrDefault(rt => rt.TechnicianId == CurrentTechnician.Id);
            if(newTechnician is null)
            { 
                r.RepairTechnicians.Add(new RepairTechnician {  RepairID = r.Id,
                    TechnicianId = CurrentTechnician.Id });
            }
            workshopClient.UpdateRepair(r);
        }
        public void UploadRepairLog(RepairLog rl)
        {        
            workshopClient.UploadRepairLog(rl);
        }
        public bool ValidateUser(User u)
        {
            var valid = workshopClient.ValidateUser(u);
            Technician tech = 
                (from techs in Technicians ?? new List<Technician>()
                 where techs.User.Username == u.Username
                 select techs).FirstOrDefault();
           
            CurrentTechnician = tech;
            return valid;
        }

        public IEnumerable<Repair> GetRepairsByTechnicianId(Technician t)
        {
            return workshopClient.GetRepairsByTechnicianId(t);
        }
        public IEnumerable<Repair> GetAvailableRepairs()
        {        
            return from availables in Repairs
                   where  availables.State == State.New ||
                     (availables.State == State.InProgress &&
                           availables.RepairTechnicians
                            .FirstOrDefault((RepairTechnician rt) => { return rt.TechnicianId == CurrentTechnician.Id; }) == null)
                   select availables;
        }

    }
}
