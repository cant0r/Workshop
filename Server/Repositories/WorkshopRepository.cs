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
            return ctx.Repairs.ToList();
        }
        public IEnumerable<Repair> GetFinishedRepairs()
        {
            using var ctx = new WorkshopContext();
            return (from repairs in ctx.Repairs
                    where repairs.State == State.Done
                    select repairs);
        }
        public IEnumerable<Repair> GetNewRepairs()
        {
            using var ctx = new WorkshopContext();
            return (from repairs in ctx.Repairs
                    where repairs.State == State.New
                    select repairs);
        }
        public IEnumerable<Repair> GetTakenRepairs()
        {
            using var ctx = new WorkshopContext();
            return (from repairs in ctx.Repairs
                    where repairs.State == State.InProgress
                    select repairs);
        }
        #endregion
        #region Logs
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
            ctx.RepairLogs.Add(log);
        }
        public void RemoveRepairLog(RepairLog log)
        {
            using var ctx = new WorkshopContext();
            ctx.RepairLogs.Remove(log);
        }
        public void UpdateRepairLog(RepairLog log)
        {
            using var ctx = new WorkshopContext();
            ctx.RepairLogs.Update(log);
        }
        #endregion
        #region ManagerGetters
        public IEnumerable<Technician> GetTechnicians()
        {
            using var ctx = new WorkshopContext();
            return ctx.Technicians.ToList();
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
       
        public IEnumerable<Client> GetClients()
        {
            using var ctx = new WorkshopContext();
            return ctx.Clients.ToList();
        }
        public IEnumerable<Auto> GetAutomobiles()
        {
            using var ctx = new WorkshopContext();
            return ctx.Automobiles.ToList();
        }
        #endregion
        public void RegisterClient(Client client)
        {
            using var ctx = new WorkshopContext();
            ctx.Clients.Add(client);
            ctx.SaveChanges();
        }
        public void RegisterAuto(Auto auto)
        {
            using var ctx = new WorkshopContext();
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
            ctx.Repairs.Update(repair);
            ctx.SaveChanges();
        }
        public void AbortRepair(Repair repair)
        {
            using var ctx = new WorkshopContext();
            var rep = ctx.Repairs.Find(repair.Id);
            rep.State = State.Cancelled;
            UpdateRepair(repair);

        }
    }
}
