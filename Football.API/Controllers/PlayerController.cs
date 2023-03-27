using Football.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        readonly FootballContext footballContext;
        public PlayerController(FootballContext footballContext)
        {
            this.footballContext = footballContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Player>> Get()
        {
            return this.Ok(footballContext.Players);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Get(int id)
        {
            var player = footballContext.Players.Find(id);
            if (player == default)
                return NotFound();
            return Ok(player);
        }

        [HttpPost]
        public ActionResult Post(Player player)
        {
            footballContext.Players.Add(player);
            footballContext.SaveChanges();
            
            return CreatedAtAction("Get", player.Id, player);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            try
            {
                footballContext.Players.Update(player);
                footballContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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
            var player = footballContext.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            footballContext.Players.Remove(player);
            footballContext.SaveChanges();

            return Ok(player);

        }


        private bool PlayerExists(int id)
        {
            return footballContext.Players.Any(e => e.Id == id);
        }
    }
}
