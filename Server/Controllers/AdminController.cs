using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ModelProvider;
using Server.Repositories;

namespace Server.Controllers
{
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
            tech.User = new GenericRepository<User>().GetAll().Single((User u) => u.Id == tech.User.Id);            
            techRepo.Add(tech, tech.User, tech.RepairTechnician);
            
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
       
        #endregion
        #region CRUDManegers
        [Route("managers")]
        [HttpPost]
        public ActionResult AddManager(Manager man)
        {
            var manRepo = new GenericRepository<Manager>();
            man.User = new GenericRepository<User>().GetAll().Single((User u) => u.Id == man.User.Id);
            manRepo.Add(man, man.User);
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
        [HttpPut]
        public ActionResult UpdateManager(Manager man)
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
       
        #endregion
        #region CRUDBonus
        [Route("bonus")]
        [HttpPost]
        public ActionResult AddBonus(Bonus bonus)
        {
            var repo = new GenericRepository<Bonus>();
            repo.Add(bonus);
            return Ok();
        }

        [Route("bonus")]
        [HttpGet]
        public ActionResult<IEnumerable<Bonus>> GetBonuses()
        {
            var them = new GenericRepository<Bonus>().GetAll();
            if (them is null)
                return NotFound();
            return Ok(them);
        }

        [Route("bonus/{id:long}")]
        [HttpGet]
        public ActionResult<Bonus> GetBonusByID(string id)
        {
            var bonus = new GenericRepository<Bonus>().Get(id);

            if (bonus is null)
                return NotFound();
            else
                return Ok(bonus);
        }
        [Route("bonus/{id}")]
        [HttpDelete]
        public ActionResult RemoveBonusByID(string id)
        {
            var repo = new GenericRepository<Bonus>();
            var bonus = repo.Get(id);
            repo.Remove(bonus);

            return Ok();
        }
        [Route("bonus")]
        [HttpPut]
        public ActionResult UpdateBonus(Bonus bonus)
        {
            var repo = new GenericRepository<Bonus>();

            if (repo.Get(bonus.Name) is null)
                AddBonus(bonus);
            else
            {
                repo.Update(bonus);
            }
            return Ok();

        }

        #endregion
        #region CRUDUsers
        [Route("users")]
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            var repo = new GenericRepository<User>();            
            repo.Add(user);           
            return Ok();
        }

        [Route("users")]
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var them = new GenericRepository<User>().GetAll();
            return Ok(them);
        }

        [Route("users/{id:long}")]
        [HttpGet]
        public ActionResult<User> GetUserByID(long id)
        {
            var user = new GenericRepository<User>().Get(id);

            if (user is null)
                return NotFound();
            else
                return Ok(user);
        }
        [Route("users/{id:long}")]
        [HttpDelete]
        public ActionResult RemoveUserByID(long id)
        {
            var repo = new GenericRepository<User>();
            var user = repo.Get(id);
            repo.Remove(user);

            return Ok();
        }


        [Route("users")]
        [HttpPut]
        public ActionResult UpdateUser(User user)
        {
            var repo = new GenericRepository<User>();

            if (repo.Get(user.Id) is null)
                AddUser(user);
            else
            {
                repo.Update(user);
            }
            return Ok();

        }
        #endregion
        [Route("clean")]
        [HttpGet]
        public ActionResult CleanRepairsAndStuff()
        {
            var repo = new GenericRepository<Auto>();
            repo.RemoveAll(repo.GetAll());
            var repo2 = new GenericRepository<Client>();
            repo2.RemoveAll(repo2.GetAll());
            var repo3 = new GenericRepository<Repair>();
            repo3.RemoveAll(repo3.GetAll());
            var repo4 = new GenericRepository<RepairLog>();
            repo4.RemoveAll(repo4.GetAll());
            var repo5 = new GenericRepository<RepairTechnician>();
            repo5.RemoveAll(repo5.GetAll());
            return Ok();
        }
    }
}
