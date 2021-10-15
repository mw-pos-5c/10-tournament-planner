namespace TournamentPlanner.DB.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsDead { get; set; }
        public Gender Gender { get; set; }
    }
}
