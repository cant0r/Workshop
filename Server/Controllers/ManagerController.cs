﻿using Microsoft.AspNetCore.Mvc;
using ModelProvider;
using Server.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



namespace Server.Controllers
{
    [Route("api/workshop/manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        [Route("repair")]
        [HttpGet]
        public ActionResult<IEnumerable<Repair>> GetAllRepairs()
        {
            var workshopRepo = new WorkshopRepository();
            var repairs = workshopRepo.GetRepairs();
            if (repairs is null)
                return NotFound();
            else
                return Ok(repairs);
        }
        [Route("repair")]
        [HttpPost]
        public ActionResult CreateRepair(Repair repair)
        {
            var workshopRepo = new WorkshopRepository();
            workshopRepo.CreateRepair(repair);
            return Ok();
        }
        [Route("repair")]
        [HttpPut]
        public ActionResult UpdateRepair([FromBody] Repair repair)
        {
            var workshopRepo = new WorkshopRepository();            
            workshopRepo.UpdateRepair(repair);
            return Ok();
        }
        [Route("repair")]
        [HttpDelete]
        public ActionResult AbortRepair(Repair repair)
        {
            var workshopRepo = new WorkshopRepository();
            workshopRepo.AbortRepair(repair);
            return Ok();
        }
        [Route("logs")]
        [HttpGet]
        public ActionResult<IEnumerable<RepairLog>> GetRepairLogs()
        {
            var workshopRepo = new WorkshopRepository();
            var repairs = workshopRepo.GetRepairLogs();
            if (repairs is null)
                return NotFound();
            else
                return Ok(repairs);
        }
        [Route("technicians")]
        [HttpGet]
        public ActionResult<IEnumerable<Technician>> GetTechnicians()
        {
            var workshopRepo = new WorkshopRepository();
            var techs = workshopRepo.GetTechnicians();
            if (techs is null)
                return NotFound();
            else
                return Ok(techs);
        }
        [Route("technicians/{repairID:long}")]
        [HttpGet]
        public ActionResult<IEnumerable<Technician>> GetTechnicians(long repairID)
        {
            var workshopRepo = new WorkshopRepository();
            var techs = workshopRepo.GetTechniciansByRepairID(repairID);
            if (techs is null)
                return NotFound();
            else
                return Ok(techs);
        }
        [Route("clients")]
        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClients()
        {
            var workshopRepo = new WorkshopRepository();
            var clients = workshopRepo.GetClients();
            if (clients is null)
                return NotFound();
            else
                return Ok(clients);
        }
        [Route("clients")]
        [HttpPost]
        public ActionResult RegisterClient(Client client)
        {
            var workshopRepo = new WorkshopRepository();
            var clients = workshopRepo.GetClients();            
            if (clients is null)
                return NotFound();
            else if (clients.Contains(client))
            {
                return Ok("Already registered.");
            }
            else
            {
                workshopRepo.RegisterClient(client);
                return Ok();
            }
               
        }
        [Route("autos")]
        [HttpGet]
        public ActionResult<IEnumerable<Auto>> GetAutomobiles()
        {
            var workshopRepo = new WorkshopRepository();
            var autos = workshopRepo.GetAutomobiles();
            if (autos is null)
                return NotFound();
            else
                return Ok(autos);
        }
        [Route("autos")]
        [HttpPost]
        public ActionResult RegisterAuto(Auto auto)
        {
            var workshopRepo = new WorkshopRepository();
            var autos = workshopRepo.GetAutomobiles();
            var clientRepo = new GenericRepository<Client>();
            auto.Client = clientRepo.GetAll().Single(c => c.Id == auto.Client.Id);
            if (autos is null)
                return NotFound();
            else if (autos.Contains(auto))
            {
                return Ok("Already registered.");
            }
            else
            {
                workshopRepo.RegisterAuto(auto);
                return Ok();
            }

        }
        [Route("bonus")]
        [HttpGet]
        public ActionResult<IEnumerable<Bonus>> GetBonuses()
        {
            var repo = new WorkshopRepository();
            var bonuses = repo.GetBonuses();
            if (bonuses is null)
                return NotFound();
            else
                return Ok(bonuses);
        }
        [Route("br")]
        [HttpGet]
        public ActionResult<IEnumerable<BonusRepair>> GetBonusRepairs()
        {
            var repo = new WorkshopRepository();
            var bonuses = repo.GetBonusRepairs();
            if (bonuses is null)
                return NotFound();
            else
                return Ok(bonuses);
        }
        [Route("managers")]
        [HttpGet]
        public ActionResult<IEnumerable<Manager>> GetManagers()
        {
            var repo = new WorkshopRepository();
            var bonuses = repo.GetManagers();
            if (bonuses is null)
                return NotFound();
            else
                return Ok(bonuses);
        }

        [Route("users")]
        [HttpPost]
        public ActionResult ValidateUser(User u)
        {
            var repo = new GenericRepository<User>();

            var result = (from users in repo.GetAll() where u.isManager == true && users.Username == u.Username
                         && System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(users.Password))
                            == u.Password select users).FirstOrDefault();


            if (result is null)
                return NotFound(false);
            else
                return Ok(true);
        }
      
    }
}
