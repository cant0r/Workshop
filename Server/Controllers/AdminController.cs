using System.Collections.Generic;
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
            if (them is null)
                return NotFound();
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
        [Route("managers")]
        [HttpPut]
        public ActionResult UpdateManagerRange(IEnumerable<Manager> man)
        {
            var repo = new GenericRepository<Manager>();
            repo.UpdateAll(man);
            return Ok();
        }
        #endregion
        #region CRUDDiscounts
        [Route("discount")]
        [HttpPost]
        public ActionResult AddDiscount(Discount discount)
        {
            var repo = new GenericRepository<Discount>();
            repo.Add(discount);
            return Ok();
        }
        [Route("discount")]
        [HttpPost]
        public ActionResult AddDiscountsRange(IEnumerable<Discount> discount)
        {
            var repo = new GenericRepository<Discount>();
            repo.AddAll(discount);
            return Ok();
        }

        [Route("discount")]
        [HttpGet]
        public ActionResult<IEnumerable<Discount>> GetDiscounts()
        {
            var them = new GenericRepository<Discount>().GetAll();
            if (them is null)
                return NotFound();
            return Ok(them);
        }

        [Route("discounts/{id:long}")]
        [HttpGet]
        public ActionResult<Discount> GetDiscountrByID(long id)
        {
            var discount = new GenericRepository<Discount>().Get(id);

            if (discount is null)
                return NotFound();
            else
                return Ok(discount);
        }
        [Route("discounts/{id:long}")]
        [HttpDelete]
        public ActionResult RemoveDiscountByID(long id)
        {
            var repo = new GenericRepository<Discount>();
            var discount = repo.Get(id);
            repo.Remove(discount);

            return Ok();
        }
        [Route("discounts")]
        [HttpDelete]
        public ActionResult RemoveDiscountRange(IEnumerable<Discount> discount)
        {
            var repo = new GenericRepository<Discount>();
            repo.RemoveAll(discount);

            return Ok();
        }

        [Route("discount")]
        [HttpPut]
        public ActionResult UpdateDiscount(Discount discount)
        {
            var repo = new GenericRepository<Discount>();

            if (repo.Get(discount.Name) is null)
                AddDiscount(discount);
            else
            {
                repo.Update(discount);
            }
            return Ok();

        }
        [Route("discount")]
        [HttpPut]
        public ActionResult UpdateDiscountRange(IEnumerable<Discount> discount)
        {
            var repo = new GenericRepository<Discount>();
            repo.UpdateAll(discount);
            return Ok();
        }
        #endregion
    }
}
