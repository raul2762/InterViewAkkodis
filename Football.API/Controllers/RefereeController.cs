using Football.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class RefereeController : ControllerBase
    {
        readonly FootballContext footballContext;
        public RefereeController(FootballContext footballContext)
        {
            this.footballContext = footballContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Referee>> Get()
        {
            return this.Ok(footballContext.Referees);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            var referee = footballContext.Referees.Find(id);
            if (referee == default)
                return NotFound();
            return Ok(referee);
        }

        [HttpPost]
        public ActionResult Post(Referee referee)
        {
            footballContext.Referees.Add(referee);
            footballContext.SaveChanges();

            return CreatedAtAction("Get", referee.Id, referee);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, Referee referee)
        {
            if (id != referee.Id)
            {
                return BadRequest();
            }

            try
            {
                footballContext.Referees.Update(referee);
                footballContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefereeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return this.Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var referee = footballContext.Referees.Find(id);
            if (referee == null)
            {
                return NotFound();
            }

            footballContext.Referees.Remove(referee);
            footballContext.SaveChanges();

            return Ok(referee);

        }

        private bool RefereeExists(int id)
        {
            return footballContext.Referees.Any(e => e.Id == id);
        }
    }
}
