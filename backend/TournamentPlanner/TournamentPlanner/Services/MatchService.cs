#region usings

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using TournamentPlanner.DB;
using TournamentPlanner.DB.Models;

#endregion

namespace TournamentPlanner.Services
{
    public class MatchService
    {
        #region Constants and Fields

        private readonly TournamentDbContext dbContext;

        #endregion

        public MatchService(TournamentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void GenerateNextMatches()
        {
            if (dbContext.Matches.FirstOrDefault(match => match.Winner == 0) != null)
            {
                throw new Exception("If there are any matches in the DB that do not have a winner, throw an exception.");
            }

            int playerCount = dbContext.Players.Count(player => !player.IsDead);

            foreach (Match match in dbContext.Matches.Include(m => m.Player1).Include(m => m.Player2))
            {
                if (match.Turns == -1)
                {
                    match.Turns = playerCount;
                }
                else
                {
                    continue;
                }

                if (match.Winner == 1)
                {
                    match.Player2.IsDead = true;
                }
                else
                {
                    match.Player1.IsDead = true;
                }
            }
            
            dbContext.SaveChanges();
            
                 
            List<Player> players = dbContext.Players.Where(player => !player.IsDead).ToList();

            int matches = players.Count / 2;
            
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
                    Turns = -1,
                };

                dbContext.Matches.Add(match);
            }

            dbContext.SaveChanges();
        }
    }
}
