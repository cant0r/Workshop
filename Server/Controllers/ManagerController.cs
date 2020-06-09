using Microsoft.AspNetCore.Mvc;
using ModelProvider;
using Server.Repositories;
using System.Collections;
using System.Collections.Generic;

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
        [Route("repair/new")]
        [HttpPost]
        public ActionResult CreateRepair(Repair repair)
        {
            var workshopRepo = new WorkshopRepository();
            workshopRepo.CreateRepair(repair);
            return Ok();
        }
        [Route("repair/update")]
        [HttpPost]
        public ActionResult UpdateRepair(Repair repair)
        {
            var workshopRepo = new WorkshopRepository();
            workshopRepo.UpdateRepair(repair);
            return Ok();
        }
        [Route("repair/abort")]
        [HttpPost]
        public ActionResult AbortRepair(Repair repair)
        {
            var workshopRepo = new WorkshopRepository();
            workshopRepo.AbortRepair(repair);
            return Ok();
        }
        //GetRepairLogs
        //GetLatestRepairLog
        //GetTechnicians
        //GetTechniciansByRepairID
        //GetClients
        //RegisterClient
        //GetAutomobiles
        //RegisterAuto

    }
}
