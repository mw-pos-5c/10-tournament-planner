#region usings

using TournamentPlanner.DB.Models;

#endregion

namespace TournamentPlanner.DTO
{
    public class MatchDto
    {
        public int Id { get; set; }

        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int Turns { get; set; }

        public int Winner { get; set; }

        public static MatchDto FromModel(Match match)
        {
            return new MatchDto
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
