using Microsoft.EntityFrameworkCore;
using ModelProvider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Repositories
{
    public class WorkshopRepository
    {
        #region Repairs
        public IEnumerable<Repair> GetRepairs()
        {
            using var ctx = new WorkshopContext();
            var repairs = ctx.Repairs.Include(r => r.Auto)
                .ThenInclude(a => a.Client)
                .Include(r => r.Bonuses)
                .Include(r => r.Manager)
                .Include(r => r.RepairTechnicians).ToList();
            return repairs;
        }
        #endregion
        #region RepairLogs
        public IEnumerable<RepairLog> GetRepairLogs()
        {
            using var ctx = new WorkshopContext();
            return (from logs in ctx.RepairLogs
                    select logs).ToList();
        }
        public IEnumerable<RepairLog> GetRepairLogs(long repairID)
        {
            using var ctx = new WorkshopContext();
            return (from logs in ctx.RepairLogs
                    where logs.Repair.Id == repairID
                    select logs).ToList();
        }
        public RepairLog GetLatestRepairLog(long repairID)
        {
            using var ctx = new WorkshopContext();
            return (from logs in ctx.RepairLogs
                    where logs.Repair.Id == repairID
                    orderby logs.Id descending
                    select logs).FirstOrDefault();
        }
        public void AddRepairLog(RepairLog log)
        {
            using var ctx = new WorkshopContext();
            var rep = ctx.Repairs.Single(r => r.Id == log.Repair.Id);
            log.Repair = rep;
            ctx.RepairLogs.Add(log);
            ctx.SaveChanges();
        }
        public void RemoveRepairLog(RepairLog log)
        {
            using var ctx = new WorkshopContext();
            ctx.RepairLogs.Remove(log);
            ctx.SaveChanges();
        }
        public void UpdateRepairLog(RepairLog log)
        {
            using var ctx = new WorkshopContext();
            ctx.RepairLogs.Update(log);
            ctx.SaveChanges();
        }
        #endregion
        #region Getters_Used_By_Manager
        public IEnumerable<Technician> GetTechnicians()
        {
            using var ctx = new WorkshopContext();
            return ctx.Technicians
                .Include(t => t.RepairTechnician).Include(r => r.User)
                .ToList();
        }
        public IEnumerable<Technician> GetTechniciansByRepairID(long id)
        {
            using var ctx = new WorkshopContext();
            var technicianIdNumbers = (from jt in ctx.RepairTechnicians.Include(rt => rt.Technician).Include(rt => rt.Repair)
                                       where jt.RepairID == id
                                       select jt.TechnicianId);
            return (from t in ctx.Technicians
                    where technicianIdNumbers.Contains(t.Id)
                    select t).ToList() ?? new List<Technician>();
        }
        public IEnumerable<Repair> GetRepairsByTechnicianID(long id)
        {
            using var ctx = new WorkshopContext();
            var technicianIdNumbers = (from jt in ctx.RepairTechnicians.Include(rt => rt.Technician).Include(rt => rt.Repair)
                                       where jt.TechnicianId == id
                                       select jt.RepairID);
            return (from t in ctx.Repairs.Include(r => r.Auto)
                                        .ThenInclude(a => a.Client)
                                        .Include(r => r.Bonuses)
                                        .Include(r => r.Manager)
                                        .Include(r => r.RepairTechnicians)
                    where technicianIdNumbers.Contains(t.Id)
                    select t).ToList() ?? new List<Repair>();
        }
        public IEnumerable<RepairTechnician> GetRepairTechnicians()
        {
            using var ctx = new WorkshopContext();
            
            return ctx.RepairTechnicians.ToList() ?? new List<RepairTechnician>();
        }
        

        public IEnumerable<Client> GetClients()
        {
            using var ctx = new WorkshopContext();
            return ctx.Clients
                .ToList();
        }
        public IEnumerable<Auto> GetAutomobiles()
        {
            using var ctx = new WorkshopContext();
            return ctx.Automobiles.Include(a => a.Client)
                .ToList();
        }
        #endregion
        #region Registration&Repair_Methods_For_Manager
        public void RegisterClient(Client client)
        {
            using var ctx = new WorkshopContext();
            ctx.Clients.Add(client);
            ctx.SaveChanges();
        }
        public void RegisterAuto(Auto auto)
        {
            using var ctx = new WorkshopContext();
            var client = ctx.Clients.Single(c => c.Id == auto.Client.Id);
            auto.Client = client;
            ctx.Automobiles.Add(auto);
            ctx.SaveChanges();
        }
        public void CreateRepair(Repair repair)
        {
            using var ctx = new WorkshopContext();
            ctx.Repairs.Add(repair);
            ctx.SaveChanges();
        }
        public void UpdateRepair(Repair repair)
        {
            using var ctx = new WorkshopContext();
            var r = ctx.Repairs.Where(er => er.Id == repair.Id)
                    .Include(er => er.RepairTechnicians)
                    .Include(er => er.Manager)
                    .Include(er => er.Bonuses)
                    .Include(er => er.Auto).ThenInclude(a => a.Client).SingleOrDefault();

            if(r != null)
            {
                foreach(RepairTechnician rt in r.RepairTechnicians)
                {
                    if(!repair.RepairTechnicians.Any(t => t.TechnicianId == rt.TechnicianId && t.RepairID == rt.RepairID))
                    {
                        ctx.RepairTechnicians.Remove(rt);
                    }
                }
                foreach (RepairTechnician rt in repair.RepairTechnicians)
                {
                    var existingRT = r.RepairTechnicians
                        .Where(t => t.TechnicianId == rt.TechnicianId && t.RepairID == rt.RepairID)
                        .SingleOrDefault();

                    if (existingRT != null)
                        ctx.Entry(existingRT).CurrentValues.SetValues(rt);
                    else
                    {
                        var newRT = new RepairTechnician
                        {
                            Repair =  rt.Repair,
                            RepairID = rt.RepairID,
                            Technician = rt.Technician,
                            TechnicianId = rt.TechnicianId
                        };
                        r.RepairTechnicians.Add(newRT);
                    }
                }
            }
            
            ctx.SaveChanges();
        }
        public void AbortRepair(Repair repair)
        {
            using var ctx = new WorkshopContext();
            var rep = ctx.Repairs.Find(repair.Id);
            rep.State = State.Cancelled;
            UpdateRepair(repair);

        }
        #endregion
       
    }
}
