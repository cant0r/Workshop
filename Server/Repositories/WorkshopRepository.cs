using Microsoft.EntityFrameworkCore;
using ModelProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

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
                .Include(r => r.BonusRepairs)
                .Include(r => r.RepairTechnicians)
                .Include(r => r.Manager).ThenInclude(r => r.User).ToList();
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
            var rep = ctx.Repairs.SingleOrDefault(r => r.Id == log.Repair.Id);
            log.Repair = rep;
            log.Repair?.Manager?.Repair?.Clear();
            ctx.RepairLogs.Add(log);
            ctx.SaveChanges();
        }

        public void UpdateRepairLog(RepairLog log)
        {
            using var ctx = new WorkshopContext();
            var repair = ctx.Repairs.SingleOrDefault(r => r.Id == log.Repair.Id);
            log.Repair = repair;
            log.Repair?.Manager?.Repair?.Clear();
            ctx.RepairLogs.Update(log);
            ctx.SaveChanges();
        }
        #endregion
        #region Getters_Used_By_Manager
        public IEnumerable<Technician> GetTechnicians()
        {
            using var ctx = new WorkshopContext();
            return ctx.Technicians
                .Include(t => t.RepairTechnicians).Include(r => r.User)
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
            var technicianIdNumbers = (from jt in ctx.RepairTechnicians
                                       .Include(rt => rt.Technician)
                                       .Include(rt => rt.Repair)
                                       where jt.TechnicianId == id
                                       select jt.RepairID);
            return (from t in ctx.Repairs.Include(r => r.Auto)
                                        .ThenInclude(a => a.Client)
                                        .Include(r => r.Manager).ThenInclude(r => r.User)
                                        .Include(r => r.RepairTechnicians)
                                        .Include(r => r.BonusRepairs)
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
           // repair.Id = 0;
            using var ctx = new WorkshopContext();
            repair.Manager.User = null;
            repair.Manager.Repair?.Clear();

            ctx.Repairs.Add(repair);
            ctx.Entry(repair.Manager).State = EntityState.Modified;
            foreach (Bonus b in repair.BonusRepairs.Select(br => br.Bonus))
                ctx.Entry(b).State = EntityState.Modified;
            ctx.SaveChanges();
        }
        public void UpdateRepair(Repair repair)
        {
            using var ctx = new WorkshopContext();
            
            var r = ctx.Repairs.SingleOrDefault(er => er.Id == repair.Id);
            ctx.Repairs.Update(r);
            var auto = ctx.Automobiles.SingleOrDefault(a => a.Id == repair.Auto.Id);
            auto.Brand = repair.Auto.Brand;
            auto.Model = repair.Auto.Model;
            auto.LicencePlate = repair.Auto.LicencePlate;
            var client = ctx.Clients.SingleOrDefault(c => c.Id == repair.Auto.Client.Id);
            client.Email = repair.Auto.Client.Email;
            client.Name = repair.Auto.Client.Name;
            client.PhoneNumber = repair.Auto.Client.PhoneNumber;
            auto.Client = client;
            var manager = ctx.Managers.SingleOrDefault(m => m.Id == repair.Manager.Id);
            r.Manager = manager;
            r.Auto = auto;
            r.State = repair.State;
            r.Description = repair.Description;
            r.Price = repair.Price;


            var rts = ctx.RepairTechnicians.Where(bp => bp.RepairID == repair.Id).ToList();
            r.RepairTechnicians?.Clear();
            foreach (RepairTechnician rt in repair.RepairTechnicians)
            {
                if (rts.SingleOrDefault(b => b.TechnicianId == rt.TechnicianId) == null)
                    rts.Add(rt);
                else
                    rts.Remove(rt);

            }
            r.RepairTechnicians = rts;

            r.BonusRepairs = ctx.BonusRepairs.Where(br => br.RepairID == r.Id).ToList();
            r.BonusRepairs?.Clear();
            foreach (Bonus b in ctx.Bonuses)
            {
                if (repair.BonusRepairs?.SingleOrDefault(br => br.BonusName == b.Name) != null)
                    r.BonusRepairs.Add(new BonusRepair { BonusName = b.Name, RepairID = r.Id });             
                
            }
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
        public IEnumerable<BonusRepair> GetBonusRepairs()
        {
            using var ctx = new WorkshopContext();
            var brs = ctx.BonusRepairs.Include(br => br.Bonus).ToList() ?? new List<BonusRepair>();
            return brs;
        }
        public IEnumerable<Bonus> GetBonuses()
        {
            using var ctx = new WorkshopContext();
            var brs = ctx.Bonuses.ToList();
            return brs;
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
