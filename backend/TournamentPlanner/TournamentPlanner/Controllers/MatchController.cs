#region usings

using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

using TournamentPlanner.DB;
using TournamentPlanner.DB.Models;
using TournamentPlanner.DTO;
using TournamentPlanner.Services;

#endregion

namespace TournamentPlanner.Controllers
{
    [ApiController]
    [Route("matches")]
    public class MatchController : ControllerBase
    {
        #region Constants and Fields

        private readonly TournamentDbContext dbContext;
        private readonly MatchesService matchesService;

        #endregion

        public MatchController(TournamentDbContext dbContext, MatchesService matchesService)
        {
            this.dbContext = dbContext;
            this.matchesService = matchesService;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateMatch([FromBody] MatchDto body)
        {
            Player? player1 = dbContext.Players.FirstOrDefault(player => player.Id == body.Player1Id);
            Player? player2 = dbContext.Players.FirstOrDefault(player => player.Id == body.Player2Id);

            if (player1 == null || player2 == null)
            {
                return BadRequest();
            }

            var match = new Match
            {
                Player1 = player1,
                Player2 = player2,
            };

            dbContext.Matches.Add(match);
            dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("active")]
        public IActionResult GetActiveMatches()
        {
            IEnumerable<MatchDto> matches = dbContext.Matches.Where(m => m.Winner == 0).Select(match => MatchDto.FromModel(match)).AsEnumerable();
            return Ok(matches);
        }

        [HttpGet]
        public IActionResult GetMatches()
        {
            return Ok(dbContext.Matches.Select(match => MatchDto.FromModel(match)));
        }

        [HttpDelete]
        public IActionResult Reset()
        {
            dbContext.Matches.RemoveRange(dbContext.Matches);
            foreach (Player player in dbContext.Players)
            {
                player.IsDead = false;
            }

            dbContext.SaveChanges();
            matchesService.GenerateNextMatches();
            return Ok();
        }

        [HttpPost]
        [Route("winner")]
        public IActionResult SetWinner([FromBody] SetWinnerDto dto)
        {
            Match? match = dbContext.Matches.FirstOrDefault(m => m.Id == dto.MatchId);
            if (match == null)
            {
                return BadRequest();
            }

            match.Winner = dto.Index;
            dbContext.SaveChanges();

            if (dbContext.Matches.FirstOrDefault(m => m.Winner == 0) == null)
            {
                matchesService.GenerateNextMatches();
                return Ok(false);
            }

            return Ok(true);
        }
    }
}
