﻿using ModelProvider;
using ModelProvider.Models;
using ModelProvider.ViewModels;
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
        public List<RepairView> Repairs { get; private set; }
        public List<TechnicianView> Technicians { get; private set; }
        public List<RepairLogView> RepairLogs { get; private set; }


        public TechnicianView CurrentTechnician { get; set; }

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
            Repairs = workshopClient.RetrieveEntities<RepairView>() ?? new List<RepairView>();
            Technicians = workshopClient.RetrieveEntities<TechnicianView>() ?? new List<TechnicianView>();
            RepairLogs = workshopClient.RetrieveEntities<RepairLogView>() ?? new List<RepairLogView>();
        }

     
        public void UpdateRepair(RepairView r)
        {
            var newTechnician = r.RepairTechnicians?.FirstOrDefault(rt => rt.TechnicianId == CurrentTechnician.Id);
            if(newTechnician is null)
            { 
                r.RepairTechnicians.Add(new RepairTechnicianView {  RepairID = r.Id,
                    TechnicianId = CurrentTechnician.Id });
            }
            workshopClient.UpdateRepair(r);
        }
        public void UploadRepairLog(RepairLogView rl)
        {        
            workshopClient.UploadRepairLog(rl);
        }
        public bool ValidateUser(UserView u)
        {
            var valid = workshopClient.ValidateUser(u);
            TechnicianView tech = 
                (from techs in Technicians ?? new List<TechnicianView>()
                 where techs.User.Username == u.Username
                 select techs).FirstOrDefault();
           
            CurrentTechnician = tech;
            return valid && tech != null;
        }

        public IEnumerable<RepairView> GetRepairsByTechnicianId(TechnicianView t)
        {
            return workshopClient.GetRepairsByTechnicianId(t);
        }
        public IEnumerable<RepairView> GetAvailableRepairs()
        {        
            var result = from availables in Repairs
                   where  availables.State == State.New ||
                     (availables.State == State.InProgress &&
                           availables.RepairTechnicians
                            .FirstOrDefault((RepairTechnicianView rt) => { return rt.TechnicianId == CurrentTechnician.Id; }) == null)
                   select availables;

            return result;
        }

    }
}
