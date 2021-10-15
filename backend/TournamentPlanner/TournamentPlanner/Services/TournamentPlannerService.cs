using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using TournamentPlanner.DB;
using TournamentPlanner.DB.Models;

namespace TournamentPlanner.Services
{
    public class TournamentPlannerService : IHostedService
    {
        private readonly ILogger<TournamentPlannerService> logger;
        private readonly IServiceScopeFactory scopeFactory;

        public TournamentPlannerService(ILogger<TournamentPlannerService> logger, IServiceScopeFactory scopeFactory)
        {
            this.logger = logger;
            this.scopeFactory = scopeFactory;
        }

        private void LoadCsvData()
        {
            using IServiceScope scope = scopeFactory.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<TournamentDbContext>();
            var matchesService = scope.ServiceProvider.GetRequiredService<MatchesService>();
            dbContext.Database.EnsureDeleted(); // <-----------------------------------------------------------------------
            dbContext.Database.EnsureCreated();
            
            using var reader = new StreamReader(Path.Combine("Resources", "players.csv")); // first_name,last_name,gender
            reader.ReadLine();

            string line;
            
            while((line = reader.ReadLine()) != null)
            {
                string[] row = line.Split(',');
                if (row.Length != 3) continue;

                if (!Enum.TryParse(row[2].ToUpper(), out Gender gender))
                {
                    continue;
                }

                var player = new Player
                {
                    IsDead = false,
                    Firstname = row[0],
                    Lastname = row[1],
                    Gender = gender
                };
                dbContext.Players.Add(player);
                logger.Log(LogLevel.Debug, $"Added Player: {player.Firstname} {player.Lastname} - {player.Gender}");
            }

            dbContext.SaveChanges();
            logger.Log(LogLevel.Information, $"Loaded {dbContext.Players.Count()} Players");
            
            matchesService.GenerateNextMatches();

        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(LoadCsvData, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
