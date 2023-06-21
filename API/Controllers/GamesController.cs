using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/games")]
    [Authorize]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameForResponceDto>>> GetGames()
        {
            return Ok(await _gameService.GetAllGamesAsync());
        }

        [HttpGet("{gameid}")]
        public async Task<ActionResult<GameForResponceDto>> GetGame(int gameid)
        {
            return Ok(await _gameService.GetGameByIdAsync(gameid));
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IActionResult> PostGame(GameForCreationDto gameForCreation)
        {
            await _gameService.AddGameAsync(gameForCreation);
            return Ok();
        }

        [Authorize(Roles = "1")]
        [HttpPut("{gameid}")]
        public async Task<IActionResult> PutGame(int gameid, GameForCreationDto gameForCreation)
        {
            await _gameService.UpdateGameAsync(gameid, gameForCreation);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpPut("{gameid}/genres/{genreid}")]
        public async Task<IActionResult> PutGenreInGame(int gameid, int genreid)
        {
            await _gameService.AddGameGenreAsync(gameid, genreid);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpPut("{gameid}/platforms/{platformid}")]
        public async Task<IActionResult> PutPlatformInGame(int gameid, int platformid)
        {
            await _gameService.AddGamePlatformAsync(gameid, platformid);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{gameid}/genres/{genreid}")]
        public async Task<IActionResult> DeleteGenreFromGame(int gameid, int genreid)
        {
            await _gameService.DeleteGameGenreAsync(gameid, genreid);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{gameid}/platforms/{platformid}")]
        public async Task<IActionResult> DeletePlatformFromGame(int gameid, int platformid)
        {
            await _gameService.DeleteGamePlatformAsync(gameid, platformid);
            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{gameid}")]
        public async Task<IActionResult> DeleteGame(int gameid)
        {
            await _gameService.DeleteGameAsync(gameid);
            return NoContent();
        }
    }
}
