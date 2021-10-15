using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using TournamentPlanner.DB;
using TournamentPlanner.DB.Models;
using TournamentPlanner.DTO;

namespace TournamentPlanner.Controllers
{
    [ApiController]
    [Route("players")]
    public class PlayerController : ControllerBase
    {
        private TournamentDbContext dbContext;

        public PlayerController(TournamentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddPlayer([FromBody] PlayerDto player)
        {
            dbContext.Players.Add(new Player
            {
                Firstname = player.Firstname,
                Lastname = player.Lastname,
                Gender = Enum.Parse<Gender>(player.Gender)
            });
            return Ok();
        }

        [HttpGet]
        public IActionResult GetPlayers()
        {
            return Ok(dbContext.Players.Select(player => PlayerDto.FromModel(player)));
        }
    }
}
