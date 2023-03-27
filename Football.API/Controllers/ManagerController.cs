using Football.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        readonly FootballContext footballContext;
        public ManagerController(FootballContext footballContext)
        {
            this.footballContext = footballContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Manager>> Get()
        {
            return this.Ok(footballContext.Managers);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            var manager = footballContext.Managers.Find(id);
            if (manager == default)
                return NotFound();
            return Ok(manager);
        }

        [HttpPost]
        public ActionResult Post(Manager manager)
        {
            footballContext.Managers.Add(manager);
            footballContext.SaveChanges();
            return CreatedAtAction("Get", manager.Id, manager);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, Manager manager)
        {
            if (id != manager.Id)
            {
                return BadRequest();
            }

            try
            {
                footballContext.Managers.Update(manager);
                footballContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var manager = footballContext.Managers.Find(id);
            if (manager == null)
            {
                return NotFound();
            }

            footballContext.Managers.Remove(manager);
            footballContext.SaveChanges();

            return Ok(manager);

        }

        private bool ManagerExists(int id)
        {
            return footballContext.Players.Any(e => e.Id == id);
        }
    }
}
