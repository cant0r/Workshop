using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelProvider;
using Server.Repositories;

namespace Server.Controllers
{

    /*
     * Add, AddAll, Get, GetAll, Remove, RemoveAll, Update, UpdateAll
     */

    [Route("api/admin/schema")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        #region CRUDTechnicians
        [Route("technicians")]
        [HttpPost]
        public ActionResult AddTechnician(Technician tech)
        {
            var techRepo = new GenericRepository<Technician>();
            techRepo.Add(tech);
            return Ok();
        }
        [Route("technicians")]
        [HttpPost]
        public ActionResult AddTechnicianRange(IEnumerable<Technician> tech)
        {
            var techRepo = new GenericRepository<Technician>();
            techRepo.AddAll(tech);
            return Ok();
        }

        [Route("technicians")]
        [HttpGet]
        public ActionResult<IEnumerable<Technician>> GetTechnicians()
        {
            var them = new GenericRepository<Technician>().GetAll();
            return Ok(them);
        }

        [Route("technicians/{id:long}")]
        [HttpGet]
        public ActionResult<Technician> GetTechnicianByID(long id)
        {
            var tech = new GenericRepository<Technician>().Get(id);

            if (tech is null)
                return NotFound();
            else
                return Ok(tech);
        }
        [Route("technicians/{id:long}")]
        [HttpDelete]
        public ActionResult RemoveTechnicianByID(long id)
        {
            var repo = new GenericRepository<Technician>();
            var tech = repo.Get(id);
            repo.Remove(tech);

            return Ok();
        }
        [Route("technicians")]
        [HttpDelete]
        public ActionResult RemoveTechnicianRange(IEnumerable<Technician> tech)
        {
            var repo = new GenericRepository<Technician>();
            repo.RemoveAll(tech);

            return Ok();
        }
        
        [Route("technicians")]
        [HttpPut]
        public ActionResult UpdateTechnician(Technician tech)
        {
            var repo = new GenericRepository<Technician>();

            if (repo.Get(tech.Id) is null)
                AddTechnician(tech);
            else
            {
                repo.Update(tech);
            }
            return Ok();
                 
        }
        [Route("technicians")]
        [HttpPut]
        public ActionResult UpdateTechnicianRange(IEnumerable<Technician> tech)
        {
            var repo = new GenericRepository<Technician>();    
            repo.UpdateAll(tech);
            return Ok();
        }
        #endregion

    }
}
