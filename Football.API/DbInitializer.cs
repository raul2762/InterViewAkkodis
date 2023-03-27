using Football.API.Models;
using System;
using System.Linq;

namespace Football.API
{
    public static class DbInitializer
    {
        public static void Initialize(FootballContext context)
        {
            context.Database.EnsureCreated();

            if (context.Players.Any())
                return;

            var players = new Player[] 
            {
                new Player{ Name = "Lionel" },
                new Player{ Name = "Cristiano" },
                new Player{ Name = "Iker" },
                new Player{ Name = "Gerard" },
                new Player{ Name = "Philippe" },
                new Player{ Name = "Jordi" }
            };

            foreach (var p in players)
                context.Players.Add(p);
            context.SaveChanges();

            var managers = new Manager[] 
            {
                new Manager { Name = "Alex" },
                new Manager { Name = "Zidane" },
                new Manager { Name = "Guardiola" }
            };

            foreach (var m in managers)
                context.Managers.Add(m);
            context.SaveChanges();

            var referees = new Referee[] 
            {
                new Referee { Name = "Pierluigi" },
                new Referee { Name = "Howard" }
            };

            foreach (var r in referees)
                context.Referees.Add(r);
            context.SaveChanges();

            var matches = new Match[]
            {
                new Match{
                    HouseManager = context.Managers.First(m => m.Name == "Alex"),
                    AwayManager = context.Managers.First(m => m.Name == "Zidane"),
                    Referee = context.Referees.First(m => m.Name == "Pierluigi") },
                new Match{
                    HouseManager = context.Managers.First(m => m.Name == "Zidane"),
                    AwayManager = context.Managers.First(m => m.Name == "Guardiola"),
                    Referee = context.Referees.First(m => m.Name == "Howard") }
            };

            foreach (var ma in matches)
                context.Matches.Add(ma);
            context.SaveChanges();
        }
    }
}
