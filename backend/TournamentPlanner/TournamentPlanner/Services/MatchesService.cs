using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using TournamentPlanner.DB;
using TournamentPlanner.DB.Models;

namespace TournamentPlanner.Services
{
    public class MatchesService
    {
        private TournamentDbContext dbContext;

        public MatchesService(TournamentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void GenerateNextMatches()
        {
            
            if (dbContext.Matches.FirstOrDefault(match => match.Winner == null) != null)
            {
                throw new Exception("If there are any matches in the DB that do not have a winner, throw an exception.");
            }

            foreach (Match match in dbContext.Matches.Include(m => m.Player1).Include(m => m.Player2))
            {
                if (match.Winner == 1)
                {
                    match.Player2.IsDead = true;
                }
                else
                {
                    match.Player1.IsDead = true;
                }
            }
            
            dbContext.Matches.RemoveRange(dbContext.Matches);

            dbContext.SaveChanges();
            
            var players = dbContext.Players.Where(player => !player.IsDead).ToList();

            int matches = players.Count / 2;

            Console.WriteLine(matches);
            
            //int count = dbContext.Matches.Count();

            var rng = new Random();

            for (var i = 0; i < matches; i++)
            {
                Player player1 = players[rng.Next(0, players.Count)];
                players.Remove(player1);
                Player player2 = players[rng.Next(0, players.Count)];
                players.Remove(player2);
                
                var match = new Match
                {
                    Player1 = player1,
                    Player2 = player2,
                    Turns = 0,
                };

                dbContext.Matches.Add(match);
            }

            dbContext.SaveChanges();

        }
    }
}
