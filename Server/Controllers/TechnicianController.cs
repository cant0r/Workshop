using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ModelProvider;
using Server.Repositories;

namespace Server.Controllers
{
    [Route("api/workshop/technician")]
    [ApiController]
    public class TechnicianController : ControllerBase
    {
        [Route("new")]
        [HttpGet]
        public ActionResult<IEnumerable<Repair>> GetNewRepairJobs()
        {
            var workshopRepo = new WorkshopRepository();
            var repairs = workshopRepo.GetNewRepairs();
            if (repairs is null)
                return NotFound();
            return Ok(repairs);
        }
        [Route("inprogress")]
        [HttpGet]
        public ActionResult<IEnumerable<Repair>> GetTakenRepairJobs()
        {
            var workshopRepo = new WorkshopRepository();
            var repairs = workshopRepo.GetTakenRepairs();
            if (repairs is null)
                return NotFound();
            return Ok(repairs);
        }
        [Route("new")]
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
        [HttpPost]
        public ActionResult AddRepairLog(RepairLog log)
        {
            var workshopRepo = new WorkshopRepository();
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
        [HttpPost]
        public ActionResult RemoveRepairLog(RepairLog log)
        {
            var workshopRepo = new WorkshopRepository();
            workshopRepo.RemoveRepairLog(log);
            return Ok();
        }
        [Route("logs")]
        [HttpPost]
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
    }
}
