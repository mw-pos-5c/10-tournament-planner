namespace TournamentPlanner.DB.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int Turns { get; set; }

        public int Winner { get; set; }
        
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
    }
}
