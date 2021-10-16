#region usings

using Microsoft.EntityFrameworkCore;

using TournamentPlanner.DB.Models;

#endregion

namespace TournamentPlanner.DB
{
    public class TournamentDbContext : DbContext
    {
        public TournamentDbContext(DbContextOptions<TournamentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Player> Players { get; set; }
    }
}
