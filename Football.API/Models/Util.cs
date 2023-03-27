using System;
using System.Linq;

namespace Football.API.Models
{
    public class Util
    {
        readonly FootballContext footballContext;
        public Util(FootballContext footballContext)
        {
            this.footballContext = footballContext;
            MinutesPlayed = GetAllMinutesPlayed();
        }

        public int MinutesPlayed { get; private set; }


        private int GetAllMinutesPlayed()
        {
            var minutesPlayed = footballContext.Players.Sum(p => p.MinutesPlayed);
            return minutesPlayed;
        }
    }
}
