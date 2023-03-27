using Football.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        readonly FootballContext footballContext;
        public StatisticsController(FootballContext footballContext)
        {
            this.footballContext = footballContext;
        }

        [HttpGet]
        [Route("yellowcards")]
        public ActionResult GetYellowCards()
        {
            Card card = new Card(footballContext);
            return Ok(card.YellowCards);
        }

        [HttpGet]
        [Route("redcards")]
        public ActionResult GetRedCards()
        {
            Card card = new Card(footballContext);
            return Ok(card.RedCards);
        }

        [HttpGet]
        [Route("minutesplayed")]
        public ActionResult GetMinutesPlayed()
        {
            Util util = new Util(footballContext);
            return Ok(util.MinutesPlayed);
        }
    }
}
