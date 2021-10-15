using TournamentPlanner.DB.Models;

namespace TournamentPlanner.DTO
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public int Turns { get; set; }
        
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }

        public int Winner { get; set; }

        public static MatchDTO FromModel(Match match)
        {
            return new MatchDTO
            {
                Id = match.Id,
                Turns = match.Turns,
                Player1Id = match.Player1Id,
                Player2Id = match.Player2Id,
                Winner = match.Winner
            };
        }

    }
}
