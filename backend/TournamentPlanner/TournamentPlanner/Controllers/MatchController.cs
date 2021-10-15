using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TournamentPlanner.DB;
using TournamentPlanner.DB.Models;
using TournamentPlanner.DTO;
using TournamentPlanner.Services;

namespace TournamentPlanner.Controllers
{
    [ApiController]
    [Route("matches")]
    public class MatchController : ControllerBase
    {
        private TournamentDbContext dbContext;
        private MatchesService matchesService;

        public MatchController(TournamentDbContext dbContext, MatchesService matchesService)
        {
            this.dbContext = dbContext;
            this.matchesService = matchesService;
        }

        [HttpGet]
        public IActionResult GetMatches()
        {
            return Ok(dbContext.Matches.Select(match => MatchDTO.FromModel(match)));
        }


        [HttpPost]
        [Route("create")]
        public IActionResult CreateMatch([FromBody] MatchDTO body)
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
        
        [HttpPost]
        [Route("winner")]
        public IActionResult SetWinner([FromBody] SetWinnerDTO dto)
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

        [HttpGet]
        [Route("active")]
        public IActionResult GetActiveMatches()
        {
            IEnumerable<MatchDTO> matches = dbContext.Matches.Where(m => m.Winner == null).Select(match => MatchDTO.FromModel(match)).AsEnumerable();
            return Ok(matches);
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
    }
}
