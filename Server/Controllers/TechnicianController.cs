using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using ModelProvider;
using ModelProvider.ViewModels;
using Server.Repositories;

namespace Server.Controllers
{
    [Route("api/workshop/technician")]
    [ApiController]
    public class TechnicianController : ControllerBase
    {
        [Route("repair")]
        [HttpGet]
        public ActionResult<IEnumerable<RepairView>> GetRepairJobs()
        {
            var workshopRepo = new WorkshopRepository();
            var repairs = workshopRepo.GetRepairs();
            if (repairs is null)
                return NotFound();
            return Ok(repairs);
        }
        [Route("repair/{techID:long}")]
        [HttpGet]
        public ActionResult<IEnumerable<RepairView>> GetRepairs(long techID)
        {
            var workshopRepo = new WorkshopRepository();
            var techs = workshopRepo.GetRepairsByTechnicianID(techID);
            if (techs is null)
                return NotFound();
            else
                return Ok(techs);
        }
        [Route("repair")]
        [HttpPut]
        public ActionResult TakeRepairJob(RepairView repair)
        {
            var workshopRepo = new WorkshopRepository();
            if (repair is null)
                return NotFound();
            workshopRepo.UpdateRepair(repair);
            return Ok();
        }     
        [Route("logs")]
        [HttpGet]
        public ActionResult<IEnumerable<RepairLogView>> GetRepairLogs()
        {
            var workshopRepo = new WorkshopRepository();
            var logs = workshopRepo.GetRepairLogs();
            if (logs is null)
                return NotFound();
            return Ok(logs);
        }
        [Route("logs")]
        [HttpPost]
        public ActionResult AddRepairLog(RepairLogView log)
        {            
            var workshopRepo = new WorkshopRepository();

            if (workshopRepo.GetRepairLogs().SingleOrDefault(rl => rl.Id == log.Id) != null)
                return Ok();

            var repairRepo = workshopRepo.GetRepairs();
            log.Repair = repairRepo.Single(r => r.Id == log.Repair.Id);
            workshopRepo.AddRepairLog(log);
            return Ok();
        }
        [Route("logs/{repairID:long}")]
        [HttpGet]
        public ActionResult<IEnumerable<RepairLogView>> GetRepairLogs(long repairID)
        {
            var workshopRepo = new WorkshopRepository();
            var logs = workshopRepo.GetRepairLogs(repairID);
            if (logs is null)
                return NotFound();
            return Ok(logs);
        }     
        [Route("logs")]
        [HttpPut]
        public ActionResult UpdateRepairLog(RepairLogView log)
        {
            var workshopRepo = new WorkshopRepository();
            RepairLogView found = workshopRepo.GetRepairLogs(log.Repair.Id).SingleOrDefault(rl => rl.Id == log.Id);
           
            
            if (found is null)
                workshopRepo.AddRepairLog(log);
            else
            {
                found.Date = DateTime.Today;
                found.Description = log.Description;
                found.TechnicianId = log.TechnicianId;
                workshopRepo.UpdateRepairLog(found);
            }
               
            return Ok();
        }
        [Route("users")]
        [HttpPost]
        public ActionResult ValidateUser(UserView u)
        {
            var repo = new WorkshopRepository().GetUsers();

            var result = (from users in repo
                          where users.Username == u.Username &&
                          System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(users.Password)) == u.Password  
                          select users).FirstOrDefault();


            if (result is null)
                return NotFound(false);
            else
                return Ok(true);
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
        public ActionResult<IEnumerable<TechnicianView>> GetTechniciansByRepairID(long repairID)
        {
            var workshopRepo = new WorkshopRepository();
            var techs = workshopRepo.GetTechniciansByRepairID(repairID);
            if (techs is null)
                return NotFound();
            else
                return Ok(techs);
        }

        
       
    }
}
