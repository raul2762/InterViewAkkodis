using System;
using System.Linq;

namespace Football.API.Models
{
    public class Card
    {
        readonly FootballContext footballContext;
        public Card(FootballContext footballContext)
        {
            this.footballContext = footballContext;
            YellowCards = GetAllYellowCard();
            RedCards = GetAllRedCard();
        }

        public Card()
        {
            YellowCards = GetAllYellowCard();
            RedCards = GetAllRedCard();
        }

        public IQueryable<Player> YellowCards { get; private set; }
        public IQueryable<Player>RedCards { get; private set; }



        private IQueryable<Player> GetAllYellowCard()
        {
            var playerYellowCard = footballContext.Players.Where(p => p.YellowCard > 0);

            return playerYellowCard;
        }

        private IQueryable<Player> GetAllRedCard()
        {
            var playerRedCard = footballContext.Players.Where(p => p.RedCard > 0);

            return playerRedCard;
        }

    }
}
