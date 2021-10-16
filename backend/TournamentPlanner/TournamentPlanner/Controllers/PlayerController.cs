#region usings

using Microsoft.AspNetCore.Mvc;

using TournamentPlanner.DTO;
using TournamentPlanner.Services;

#endregion

namespace TournamentPlanner.Controllers
{
    [ApiController]
    [Route("players")]
    public class PlayerController : ControllerBase
    {
        #region Constants and Fields
        
        private readonly PlayerControllerService playerService;

        #endregion

        public PlayerController(PlayerControllerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddPlayer([FromBody] PlayerDto player)
        {
            playerService.AddPlayer(player);
            return Ok();
        } 

        [HttpGet]
        public IActionResult GetPlayers()
        {
            return Ok(playerService.GetPlayers());
        }

        [HttpPost]
        [Route("gender")]
        public IActionResult SetGender([FromBody] SetGenderDto dto)
        {
            if (playerService.SetGender(dto))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
