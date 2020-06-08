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
        public IEnumerable<Auto> GetAutomobiles()
        {
            using var ctx = new WorkshopContext();
            return ctx.Automobiles.ToList();
        }
        public void RegisterClient(Client client)
        {
            using var ctx = new WorkshopContext();
            ctx.Clients.Add(client);
            ctx.SaveChanges();
        }
        public void RegisterAuto(Auto auto)
        {
            using var ctx = new WorkshopContext();

            var validOwner = (from client in ctx.Clients
                              where client.Id == auto.Client.Id
                              select client).FirstOrDefault();
            if (validOwner is null)
            {
                // TODO Log invalid owner id
                return;
            }

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
        public void RemoveRepair(Repair repair, string controlMessage)
        {
            using var ctx = new WorkshopContext();
            var rep = ctx.Repairs.Find(repair.Id);

            if (controlMessage == rep.Id.ToString())
            {
                rep.State = State.Cancelled;
                UpdateRepair(repair);
            }
        }
    }
}
