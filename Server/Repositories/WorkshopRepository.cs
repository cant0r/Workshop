using Microsoft.EntityFrameworkCore;
using ModelProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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
                .Include(r => r.BonusRepairs).ThenInclude(br => br.Bonus)
                .Include(r => r.Manager).ThenInclude(m => m.User)
                .Include(r => r.RepairTechnicians).ToList();
            return repairs;
        }
        #endregion
        #region RepairLogs
        public IEnumerable<RepairLog> GetRepairLogs()
        {
            using var ctx = new WorkshopContext();
            return (from logs in ctx.RepairLogs.Include(rl => rl.Repair)
                    select logs).ToList();
        }
        public IEnumerable<RepairLog> GetRepairLogs(long repairID)
        {
            using var ctx = new WorkshopContext();
            var ls = (from logs in ctx.RepairLogs.Include(rl => rl.Repair)
                    where logs.Repair.Id == repairID
                    select logs).ToList();

            return ls;
            
        }
        public RepairLog GetLatestRepairLog(long repairID)
        {
            using var ctx = new WorkshopContext();
            return (from logs in ctx.RepairLogs.Include(rl => rl.Repair)
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
            var repair = ctx.Repairs.SingleOrDefault(r => r.Id == log.Repair.Id);
            log.Repair = repair;
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
                                        .Include(r => r.BonusRepairs).ThenInclude(br => br.Bonus)
                                        .Include(r => r.Manager).ThenInclude(m => m.User)
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
            var manager = ctx.Managers.FirstOrDefault(m => m.Id == repair.Manager.Id);
            repair.Manager = manager;

            foreach(BonusRepair br in repair.BonusRepairs)
            {
                ctx.Entry(br.Bonus).State = EntityState.Detached;
            }

            ctx.Entry(repair.Manager).State = EntityState.Modified;
            ctx.Entry(repair.Manager.User).State = EntityState.Modified;
            ctx.SaveChanges();
        }
        public void UpdateRepair(Repair repair)
        {
            using var ctx = new WorkshopContext();
            var r = ctx.Repairs.Single(er => er.Id == repair.Id);
            var auto = ctx.Automobiles.SingleOrDefault(a => a.Id == repair.Auto.Id);
            var client = ctx.Clients.SingleOrDefault(c => c.Id == repair.Auto.Client.Id);
            auto.Client = client;

            var manager = ctx.Managers?.SingleOrDefault(m => m.Id == repair.Manager.Id);

            r.Auto = auto;
            r.Manager = manager;
            r.State = repair.State;

            var rts = new List<RepairTechnician>();
            var rete = ctx.RepairTechnicians.Where(rt => rt.RepairID == repair.Id);
            foreach(RepairTechnician rt in rete)
            {
                    rts.Add(rt);
            }
            r.RepairTechnicians = rts;

            var rts2 = new List<BonusRepair>();
            var rete2 = ctx.BonusRepairs.Where(rt => rt.RepairID == repair.Id);
            foreach (BonusRepair rt in rete2)
            {
                rts2.Add(rt);
            }
            r.BonusRepairs = rts2;

            ctx.Entry(r).State = EntityState.Modified;                      
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
        public IEnumerable<Manager> GetManagers()
        {
            using var ctx = new WorkshopContext();
            var repairs = ctx.Managers.Include(m => m.User).ToList();
            return repairs;
        }
    }
}
