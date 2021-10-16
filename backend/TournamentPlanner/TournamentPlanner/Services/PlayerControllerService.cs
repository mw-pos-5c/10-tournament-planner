using System;
using System.Collections.Generic;
using System.Linq;

using TournamentPlanner.DB;
using TournamentPlanner.DB.Models;
using TournamentPlanner.DTO;

namespace TournamentPlanner.Services
{
    public class PlayerControllerService
    {
        private readonly TournamentDbContext dbContext;

        public PlayerControllerService(TournamentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddPlayer(PlayerDto dto)
        {
            dbContext.Players.Add(new Player
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                Gender = Enum.Parse<Gender>(dto.Gender ?? throw new ArgumentException())
            });
        }

        public IEnumerable<PlayerDto> GetPlayers()
        {
            return dbContext.Players.Select(player => PlayerDto.FromModel(player)).AsEnumerable();
        }

        public bool SetGender(SetGenderDto dto)
        {
            Player? player = dbContext.Players.FirstOrDefault(p => p.Id == dto.PlayerId);
            if (player == null)
            {
                return false;
            }
            player.Gender = Enum.Parse<Gender>(dto.Gender ?? throw new ArgumentException());
            dbContext.SaveChanges();
            return true;
        }

    }
}
