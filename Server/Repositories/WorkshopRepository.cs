﻿using Microsoft.EntityFrameworkCore;
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
            return ctx.Repairs.Include(r => r.Auto).ThenInclude(a => a.Client).Include(r => r.Bonuses).Include(r => r.RepairTechnicians).Include(r => r.Manager).ToList();
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
            return (from t in ctx.Repairs
                    where technicianIdNumbers.Contains(t.Id)
                    select t).ToList() ?? new List<Repair>();
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
        #endregion
       
    }
}
