using Microsoft.EntityFrameworkCore;
using ModelProvider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Repositories
{
    public class WorkshopRepository
    {
       public IEnumerable<Repair> GetRepairsInProgress()
        {
            using var ctx = new WorkshopContext();
            return ctx.Repairs.ToList();
        }
       public IEnumerable<Technician> GetTechniciansByRepairID(long id)
        {
            using var ctx = new WorkshopContext();
            var technicianIdNumbers = (from jt in ctx.RepairTechnicians
                                       where jt.RepairID == id
                                       select jt.TechnicianId);
            return (from t in ctx.Technicians
                    where technicianIdNumbers.Contains(t.Id)
                    select t);
        }
     
       public RepairLog GetRepairLogByRepairID(long id)
        {
            using var ctx = new WorkshopContext();
            return (from logs in ctx.RepairLogs
                    where logs.Id == id
                    select logs).FirstOrDefault();
        }
      
       public IEnumerable<Technician> GetTechnicians()
        {
            using var ctx = new WorkshopContext();
            return ctx.Technicians.ToList();
        }
        public IEnumerable<Repair> GetFinishedRepairs()
        {
            using var ctx = new WorkshopContext();
            return (from repairs in ctx.Repairs
                    where repairs.State == State.Done
                    select repairs);
        }
        public IEnumerable<Client> GetClients()
        {
            using var ctx = new WorkshopContext();
            return ctx.Clients.ToList();
        }
            //Get cars (police.hu)
        public IEnumerable<Auto> GetAutomobiles()
        {
            using var ctx = new WorkshopContext();
            return ctx.Automobiles.ToList();
        }

            //Creations of jobs:
            //1. Register Client
            //2. Register Auto
            //3. Updated JoinTable if necessary
            //4. Create job, check for valid manager

            //Update jobs:
            //1. Using an overview form

            //Delete jobs: inside database form
            //For confirmation the job id must be given via a messageboc thingy
            //the server receives said id



        }
}
