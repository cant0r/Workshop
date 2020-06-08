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
        #region CRUDManegers
        [Route("managers")]
        [HttpPost]
        public ActionResult AddManager(Manager man)
        {
            var manRepo = new GenericRepository<Manager>();
            manRepo.Add(man);
            return Ok();
        }
        [Route("managers")]
        [HttpPost]
        public ActionResult AddManagerRange(IEnumerable<Manager> man)
        {
            var manRepo = new GenericRepository<Manager>();
            manRepo.AddAll(man);
            return Ok();
        }

        [Route("managers")]
        [HttpGet]
        public ActionResult<IEnumerable<Manager>> GetManagers()
        {
            var them = new GenericRepository<Manager>().GetAll();
            return Ok(them);
        }

        [Route("managers/{id:long}")]
        [HttpGet]
        public ActionResult<Manager> GetManagerByID(long id)
        {
            var man = new GenericRepository<Manager>().Get(id);

            if (man is null)
                return NotFound();
            else
                return Ok(man);
        }
        [Route("managers/{id:long}")]
        [HttpDelete]
        public ActionResult RemoveManagerByID(long id)
        {
            var repo = new GenericRepository<Manager>();
            var man = repo.Get(id);
            repo.Remove(man);

            return Ok();
        }
        [Route("managers")]
        [HttpDelete]
        public ActionResult RemoveManagerRange(IEnumerable<Manager> man)
        {
            var repo = new GenericRepository<Manager>();
            repo.RemoveAll(man);

            return Ok();
        }

        [Route("managers")]
        [HttpPut]
        public ActionResult UpdateTechnician(Manager man)
        {
            var repo = new GenericRepository<Manager>();

            if (repo.Get(man.Id) is null)
                AddManager(man);
            else
            {
                repo.Update(man);
            }
            return Ok();

        }
        [Route("managers")]
        [HttpPut]
        public ActionResult UpdateManagerRange(IEnumerable<Manager> man)
        {
            var repo = new GenericRepository<Manager>();
            repo.UpdateAll(man);
            return Ok();
        }
        #endregion
    }
}
