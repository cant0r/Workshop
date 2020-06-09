using Microsoft.AspNetCore.Mvc;
using ModelProvider;
using Server.Repositories;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Server.Controllers
{
    [Route("api/workshop/manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        [Route("repair/all")]
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
        [Route("logs/{repairID:long}")]
        [HttpGet]
        public ActionResult<IEnumerable<RepairLog>> GetRepairLogs(long repairID)
        {
            var workshopRepo = new WorkshopRepository();
            var repairs = workshopRepo.GetRepairLogs(repairID);
            if (repairs is null)
                return NotFound();
            else
                return Ok(repairs);
        }
        [Route("logs/{repairID:long}/latest")]
        [HttpGet]
        public ActionResult<RepairLog> GetLatestRepairLog(long repairID)
        {
            var workshopRepo = new WorkshopRepository();
            var repair = workshopRepo.GetLatestRepairLog(repairID);
            if (repair is null)
                return NotFound();
            else
                return Ok(repair);
        }
        [Route("technicians/all")]
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
        [Route("clients/all")]
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
        [Route("clients/register")]
        [HttpGet]
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
        [Route("autos/all")]
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
        [Route("autos/register")]
        [HttpGet]
        public ActionResult RegisterAuto(Auto auto)
        {
            var workshopRepo = new WorkshopRepository();
            var autos = workshopRepo.GetAutomobiles();
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

    }
}
