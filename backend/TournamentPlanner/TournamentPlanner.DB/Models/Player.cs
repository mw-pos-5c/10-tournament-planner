namespace TournamentPlanner.DB.Models
{
    public class Player
    {
        public string Firstname { get; set; }
        public Gender Gender { get; set; }
        public int Id { get; set; }
        public bool IsDead { get; set; }
        public string Lastname { get; set; }
    }
}
