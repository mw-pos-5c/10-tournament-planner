#region usings

using TournamentPlanner.DB.Models;

#endregion

namespace TournamentPlanner.DTO
{
    public class PlayerDto
    {
        public string? Firstname { get; set; }
        public string? Gender { get; set; }
        public int Id { get; set; }
        public bool IsDead { get; set; }
        public string? Lastname { get; set; }

        public static PlayerDto FromModel(Player player)
        {
            return new PlayerDto
            {
                Id = player.Id,
                IsDead = player.IsDead,
                Firstname = player.Firstname,
                Lastname = player.Lastname,
                Gender = player.Gender.ToString()
            };
        }
    }
}
