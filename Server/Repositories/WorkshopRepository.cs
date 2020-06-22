using Microsoft.EntityFrameworkCore;
using ModelProvider.Models;
using ModelProvider.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Repositories
{
    public class WorkshopRepository
    {
        #region Repairs
        public IEnumerable<RepairView> GetRepairs()
        {
            using var ctx = new WorkshopContext();
            var repairs = ctx.Repairs.Include(r => r.Auto)
                .ThenInclude(a => a.Client)
                .Include(r => r.BonusRepairs)
                .Include(r => r.RepairTechnicians)
                .Include(r => r.Manager).ThenInclude(r => r.User).ToList();
            var rps = repairs.Select(r => ViewMapper.GetViewModell(r)).ToList();
            return rps;
        }
        #endregion
        #region RepairLogs
        public IEnumerable<RepairLogView> GetRepairLogs()
        {
            using var ctx = new WorkshopContext();
            return (from logs in ctx.RepairLogs.Include(rl => rl.Repair)
                    select ViewMapper.GetViewModell(logs)).ToList();
        }
        public IEnumerable<RepairLogView> GetRepairLogs(long repairID)
        {
            using var ctx = new WorkshopContext();
            var ls = (from logs in ctx.RepairLogs.Include(rl => rl.Repair)
                      where logs.Repair.Id == repairID
                      select ViewMapper.GetViewModell(logs)).ToList();

            return ls;

        }
        public void AddRepairLog(RepairLogView log)
        {
            using var ctx = new WorkshopContext();
            RepairLog newLog = new RepairLog();
            newLog.Date = log.Date;
            newLog.Description = log.Description;
            newLog.Repair = ctx.Repairs.SingleOrDefault(r => r.Id == log.Repair.Id);
            newLog.TechnicianId = log.TechnicianId;     
            ctx.RepairLogs.Add(newLog);
            ctx.SaveChanges();
        }

        public void UpdateRepairLog(RepairLogView log)
        {
            using var ctx = new WorkshopContext();
            RepairLog updatedLog = ctx.RepairLogs.Single(rl => rl.Id == log.Id);
            updatedLog.Repair = ctx.Repairs.SingleOrDefault(r => r.Id == log.Repair.Id);
            updatedLog.Id = log.Id;
            updatedLog.Description = log.Description;
            updatedLog.Date = log.Date;
            updatedLog.TechnicianId = log.TechnicianId;
            ctx.RepairLogs.Update(updatedLog);
            ctx.SaveChanges();
        }
        #endregion
        #region For_Manager
        public void AddTechnician(TechnicianView tv)
        {
            using var ctx = new WorkshopContext();
            Technician t = new Technician();
          
            t.Name = tv.Name;
            t.PhoneNumber = tv.PhoneNumber;
            t.User = ctx.Users.Single(u => u.Id == tv.User.Id);
            t.UserId = t.User.Id;
        }
        public TechnicianView GetTechnician(long id)
        {
            using var ctx = new WorkshopContext();
            return ViewMapper.GetViewModell(ctx.Technicians.SingleOrDefault(t => t.Id == id));
        }
        public void RemoveTechnician(long id)
        {
            using var ctx = new WorkshopContext();
            ctx.Technicians.Remove(ctx.Technicians.SingleOrDefault(t => t.Id == id));
        }
        public void UpdateTechnician(TechnicianView tv)
        {
            using var ctx = new WorkshopContext();
            var t = ctx.Technicians.Single(t => t.Id == tv.Id);
            t.Id = tv.Id;
            t.Name = tv.Name;
            t.PhoneNumber = tv.PhoneNumber;
            t.UserId = tv.UserId;           
        }
        public IEnumerable<TechnicianView> GetTechnicians()
        {
            using var ctx = new WorkshopContext();
            return ctx.Technicians
                .Include(t => t.RepairTechnicians).Include(r => r.User).Select(t => ViewMapper.GetViewModell(t)).ToList();
               ;
        }
        public IEnumerable<TechnicianView> GetTechniciansByRepairID(long id)
        {
            using var ctx = new WorkshopContext();
            var technicianIdNumbers = (from jt in ctx.RepairTechnicians.Include(rt => rt.Technician).Include(rt => rt.Repair)
                                       where jt.RepairID == id
                                       select jt.TechnicianId);
            return (from t in ctx.Technicians
                    where technicianIdNumbers.Contains(t.Id)
                    select ViewMapper.GetViewModell(t)).ToList() ?? new List<TechnicianView>();
        }
        public IEnumerable<RepairView> GetRepairsByTechnicianID(long id)
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
                    select ViewMapper.GetViewModell(t)).ToList() ?? new List<RepairView>();
        }
        public IEnumerable<RepairTechnicianView> GetRepairTechnicians()
        {
            using var ctx = new WorkshopContext();

            return ctx.RepairTechnicians.Select(rt => ViewMapper.GetViewModell(rt)).ToList()
                   ?? new List<RepairTechnicianView>();
        }


        public IEnumerable<ClientView> GetClients()
        {
            using var ctx = new WorkshopContext();
            return ctx.Clients.Select(c => ViewMapper.GetViewModell(c))
                .ToList();
        }
        public IEnumerable<AutoView> GetAutomobiles()
        {
            using var ctx = new WorkshopContext();
            return ctx.Automobiles.Include(a => a.Client).Select(a => ViewMapper.GetViewModell(a))
                .ToList();
        }
        public bool ValidatePlate(string plate)
        {
            using var ctx = new WorkshopContext();
            if (ctx.Automobiles.SingleOrDefault(a => a.LicencePlate == plate) != null)
                return false;
            return true;
        }
        public bool ValidateClientEmail(string m)
        {
            using var ctx = new WorkshopContext();
            if (ctx.Clients.SingleOrDefault(a => a.Email == m) != null)
                return false;
            return true;
        }
        #endregion
        #region Registration&Repair_Methods_For_Manager
        public void RegisterClient(ClientView clientView)
        {
            using var ctx = new WorkshopContext();
            Client client = new Client();
            client.Email = clientView.Email;
            client.Name = clientView.Name;
            client.PhoneNumber = clientView.PhoneNumber;           
            ctx.Clients.Add(client);
            ctx.SaveChanges();
        }
        public void RegisterAuto(AutoView autoView)
        {
            using var ctx = new WorkshopContext();
            var validCar = ctx.Automobiles.SingleOrDefault(a => a.LicencePlate == autoView.LicencePlate);
            if (validCar != null)
            {
                ctx.Entry(validCar).State = EntityState.Modified;
                return;
            }
                
            RegisterClient(autoView.Client);
            Auto auto = new Auto();
            auto.Brand = autoView.Brand;
            auto.Model = autoView.Model;
            auto.LicencePlate = autoView.LicencePlate;
            auto.Client = ctx.Clients.SingleOrDefault(c => c.Email == autoView.Client.Email);
            ctx.Automobiles.Add(auto);
            ctx.SaveChanges();
        }
        public void CreateRepair(RepairView repairView)
        {
            RegisterAuto(repairView.Auto);
            using var ctx = new WorkshopContext();           
            Repair repair = new Repair();
            repair.Description = repairView.Description;
            repair.Manager = ctx.Managers.Single(m => m.Id == repairView.Manager.Id);
            repair.Price = repairView.Price;
            repair.State = repairView.State;
            repair.Auto = ctx.Automobiles.SingleOrDefault(a => a.LicencePlate == repairView.Auto.LicencePlate);
            ctx.Repairs.Add(repair);
            repair.BonusRepairs = new List<BonusRepair>();
            foreach(string bonus in repairView.BonusRepairs.Select(br => br.BonusName))
            {
                repair.BonusRepairs.Add(new BonusRepair { BonusName = bonus, RepairID = repair.Id });
            }

            ctx.Entry(repair.Auto).State = EntityState.Modified;
            ctx.SaveChanges();
        }
        public void UpdateRepair(RepairView repairView)
        {
            using var ctx = new WorkshopContext();          
            var r = ctx.Repairs.Include(r => r.BonusRepairs)
                                .Include(r => r.RepairTechnicians)
                                .SingleOrDefault(er => er.Id == repairView.Id);
            ctx.Repairs.Update(r);
            var auto = ctx.Automobiles.SingleOrDefault(a => a.Id == repairView.Auto.Id);
            auto.Brand = repairView.Auto.Brand;
            auto.Model = repairView.Auto.Model;
            auto.LicencePlate = repairView.Auto.LicencePlate;
            var client = ctx.Clients.SingleOrDefault(c => c.Id == repairView.Auto.Client.Id);
            client.Email = repairView.Auto.Client.Email;
            client.Name = repairView.Auto.Client.Name;
            client.PhoneNumber = repairView.Auto.Client.PhoneNumber;
            auto.Client = client;
            var manager = ctx.Managers.SingleOrDefault(m => m.Id == repairView.Manager.Id);
            r.Manager = manager;
            r.Auto = auto;
            r.State = repairView.State;
            r.Description = repairView.Description;
            r.Price = repairView.Price;           

            r.RepairTechnicians?.Clear();
            foreach (Technician tech in ctx.Technicians)
            {
                if (repairView.RepairTechnicians?.SingleOrDefault(b => b.TechnicianId == tech.Id) != null)
                    r.RepairTechnicians.Add(new RepairTechnician { RepairID = r.Id, TechnicianId = tech.Id });
            }
            
          
            r.BonusRepairs?.Clear();
            foreach (Bonus b in ctx.Bonuses)
            {
                if (repairView.BonusRepairs?.SingleOrDefault(br => br.BonusName == b.Name) != null)
                    r.BonusRepairs.Add(new BonusRepair { BonusName = b.Name, RepairID = r.Id });             
                
            }
            ctx.Entry(r).State = EntityState.Modified;
            ctx.SaveChanges();
        }
        
        public IEnumerable<BonusRepairView> GetBonusRepairs()
        {
            using var ctx = new WorkshopContext();
            var brs = ctx.BonusRepairs.Include(br => br.Bonus).Select(br => ViewMapper.GetViewModell(br)).ToList()
                ?? new List<BonusRepairView>();
            return brs;
        }
        public IEnumerable<BonusView> GetBonuses()
        {
            using var ctx = new WorkshopContext();
            var brs = ctx.Bonuses.Select(b => ViewMapper.GetViewModell(b)).ToList();
            return brs;
        }
        #endregion
        public IEnumerable<ManagerView> GetManagers()
        {
            using var ctx = new WorkshopContext();
            var managers = ctx.Managers.Include(m => m.User).ToList();
            return managers.Select(m => ViewMapper.GetViewModell(m)).ToList();
        }
        public IEnumerable<UserView> GetUsers()
        {
            using var ctx = new WorkshopContext();
            var users = ctx.Users;
            return users.Select(u => ViewMapper.GetViewModell(u)).ToList();
        }
    }
}
