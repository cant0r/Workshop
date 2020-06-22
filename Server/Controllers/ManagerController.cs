using Microsoft.AspNetCore.Mvc;
using ModelProvider.ViewModels;
using ModelProvider.Models;
using Server.Repositories;
using System;
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
        public ActionResult<IEnumerable<RepairView>> GetAllRepairs()
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
        public ActionResult CreateRepair(RepairView repair)
        {
            var workshopRepo = new WorkshopRepository();
            workshopRepo.CreateRepair(repair);
            return Ok();
        }
        [Route("repair")]
        [HttpPut]
        public ActionResult UpdateRepair(RepairView repair)
        {
            var workshopRepo = new WorkshopRepository();            
            workshopRepo.UpdateRepair(repair);
            return Ok();
        }       
        [Route("logs")]
        [HttpGet]
        public ActionResult<IEnumerable<RepairLogView>> GetRepairLogs()
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
        public ActionResult<IEnumerable<TechnicianView>> GetTechnicians()
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
        public ActionResult<IEnumerable<TechnicianView>> GetTechnicians(long repairID)
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
        public ActionResult<IEnumerable<ClientView>> GetClients()
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
        public ActionResult RegisterClient(ClientView client)
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
        [Route("clients/email/{m}")]
        [HttpGet]
        public ActionResult ValidateClientEmail(string m)
        {
            var workshopRepo = new WorkshopRepository();
            var autos = workshopRepo.ValidateClientEmail(m);
            if (!autos)
                return NotFound(false);
            else
                return Ok(true);
        }
        [Route("autos")]
        [HttpGet]
        public ActionResult<IEnumerable<AutoView>> GetAutomobiles()
        {
            var workshopRepo = new WorkshopRepository();
            var autos = workshopRepo.GetAutomobiles();
            if (autos is null)
                return NotFound();
            else
                return Ok(autos);
        }

        [Route("autos/plate/{plate}")]
        [HttpGet]
        public ActionResult ValidatePlate(string plate)
        {
            var workshopRepo = new WorkshopRepository();
            var autos = workshopRepo.ValidatePlate(plate);
            if (!autos)
                return NotFound(false);
            else
                return Ok(true);
        }

        [Route("bonus")]
        [HttpGet]
        public ActionResult<IEnumerable<BonusView>> GetBonuses()
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
        public ActionResult<IEnumerable<BonusRepairView>> GetBonusRepairs()
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
        public ActionResult<IEnumerable<ManagerView>> GetManagers()
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
        public ActionResult ValidateUser(UserView u)
        {
            var repo = new WorkshopRepository().GetUsers();

            var result = (from users in repo where u.isManager == true && users.Username == u.Username
                         && System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(users.Password))
                            == u.Password select users).FirstOrDefault();

            if (result is null)
                return NotFound(false);
            else
                return Ok(true);
        }
      
    }
}
