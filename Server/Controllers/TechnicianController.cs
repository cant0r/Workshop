using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ModelProvider;
using Server.Repositories;

namespace Server.Controllers
{
    [Route("api/workshop/technician")]
    [ApiController]
    public class TechnicianController : ControllerBase
    {
        [Route("repair")]
        [HttpGet]
        public ActionResult<IEnumerable<Repair>> GetRepairJobs()
        {
            var workshopRepo = new WorkshopRepository();
            var repairs = workshopRepo.GetRepairs();
            if (repairs is null)
                return NotFound();
            return Ok(repairs);
        }
        [Route("repair/{techID:long}")]
        [HttpGet]
        public ActionResult<IEnumerable<Repair>> GetRepairs(long techID)
        {
            var workshopRepo = new WorkshopRepository();
            var techs = workshopRepo.GetRepairsByTechnicianID(techID);
            if (techs is null)
                return NotFound();
            else
                return Ok(techs);
        }
        [Route("repair")]
        [HttpPost]
        public ActionResult TakeRepairJob(Repair repair)
        {
            var workshopRepo = new WorkshopRepository();
            if (repair is null)
                return NotFound();
            workshopRepo.UpdateRepair(repair);
            return Ok();
        }
        [Route("logs")]
        [HttpGet]
        public ActionResult<IEnumerable<RepairLog>> GetRepairLogs()
        {
            var workshopRepo = new WorkshopRepository();
            var logs = workshopRepo.GetRepairLogs();
            if (logs is null)
                return NotFound();
            return Ok(logs);
        }
        [Route("logs")]
        [HttpPost]
        public ActionResult AddRepairLog(RepairLog log)
        {
            var workshopRepo = new WorkshopRepository();
            var repairRepo = new GenericRepository<Repair>();
            log.Repair = repairRepo.GetAll().Single(r => r.Id == log.Repair.Id);
            workshopRepo.AddRepairLog(log);
            return Ok();
        }
        [Route("logs/{repairID:long}")]
        [HttpGet]
        public ActionResult<IEnumerable<RepairLog>> GetRepairLogs(long repairID)
        {
            var workshopRepo = new WorkshopRepository();
            var logs = workshopRepo.GetRepairLogs(repairID);
            if (logs is null)
                return NotFound();
            return Ok(logs);
        }
        [Route("logs")]
        [HttpDelete]
        public ActionResult RemoveRepairLog(RepairLog log)
        {
            var workshopRepo = new WorkshopRepository();
            workshopRepo.RemoveRepairLog(log);
            return Ok();
        }
        [Route("logs")]
        [HttpPut]
        public ActionResult UpdateRepairLog(RepairLog log)
        {
            var workshopRepo = new WorkshopRepository();
            var found = new GenericRepository<Repair>().Get(log.Repair.Id);
            if (found is null)
                workshopRepo.AddRepairLog(log);
            else
                workshopRepo.UpdateRepairLog(log);
            return Ok();
        }
        [Route("users")]
        [HttpPost]
        public ActionResult ValidateUser(User u)
        {
            var repo = new GenericRepository<User>();

            var result = (from users in repo.GetAll()
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
        public ActionResult<IEnumerable<Technician>> GetTechniciansByRepairID(long repairID)
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
