using Microsoft.EntityFrameworkCore;

using TournamentPlanner.DB.Models;

namespace TournamentPlanner.DB
{
    public class TournamentDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=data.db;");
        }
        
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }

    }
}
