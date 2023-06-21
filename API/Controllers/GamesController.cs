using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DAL.Data;
using BLL.Dtos.OutDto;
using BLL.Dtos.InDto;
using DAL.Models;

namespace API.Controllers
{
    [Route("api/games")]
    [Authorize]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GameStoreDBContext _context;
        private readonly IMapper _mapper;

        public GamesController(GameStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameForResponceDto>>> GetGames()
        {
            if (_context.Games == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<Game>, List<GameForResponceDto>>(await _context.Games.ToListAsync()));
        }

        [HttpGet("{gameid}")]
        public async Task<ActionResult<GameForResponceDto>> GetGame(int gameid)
        {
            if (_context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(gameid);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Game, GameForResponceDto>(game));
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<ActionResult<GameForResponceDto>> PostGame(GameForCreationDto game)
        {
            if (game is null)
            {
                return BadRequest("Game object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var mappedGame = _mapper.Map<Game>(game);

            _context.Games.Add(mappedGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = mappedGame.Id }, _mapper.Map<Game, GameForResponceDto>(mappedGame));
        }

        [Authorize(Roles = "1")]
        [HttpPut("{gameid}")]
        public async Task<IActionResult> PutGame(int gameid, GameForCreationDto game)
        {
            if (game is null)
            {
                return BadRequest("Game object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var gameEntity = _context.Games.Find(gameid);
            if (gameEntity is null)
            {
                return NotFound();
            }

            _mapper.Map(game, gameEntity);
            _context.Games.Update(gameEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpPut("{gameid}/genres/{genreid}")]
        public async Task<IActionResult> PutGenreInGame(int gameid, int genreid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var gameEntity = await _context.Games.FindAsync(gameid);
            if (gameEntity is null)
            {
                return NotFound();
            }

            var genreEntity = await _context.Genres.FindAsync(genreid);
            if (genreEntity is null)
            {
                return NotFound();
            }

            gameEntity.Genres.Add(genreEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpPut("{gameid}/platforms/{platformid}")]
        public async Task<IActionResult> PutPlatformInGame(int gameid, int platformid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var gameEntity = await _context.Games.FindAsync(gameid);

            if (gameEntity is null)
            {
                return NotFound();
            }
            var platformEntity = await _context.Platforms.FindAsync(platformid);

            if (platformEntity is null)
            {
                return NotFound();
            }

            gameEntity.Platforms.Add(platformEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{gameid}/genres/{genreid}")]
        public async Task<IActionResult> DeleteGenreFromGame(int gameid, int genreid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var genreEntity = await _context.Genres.FindAsync(genreid);
            if (genreEntity is null)
            {
                return NotFound("Genre with this id not found");
            }

            var gameEntity = _context.Games.Include(g => g.Genres.Where(g => g.Id == genreid)).Where(g => g.Id == gameid).FirstOrDefault();

            if (gameEntity is null)
            {
                return NotFound("Game with this id not found");
            }

            if (!gameEntity.Genres.Any(g => g.Id == genreid))
            {
                return NotFound("Game with this id doesn't have genre with this id");
            }

            gameEntity.Genres.Remove(genreEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{gameid}/platforms/{platformid}")]
        public async Task<IActionResult> DeletePlatformFromGame(int gameid, int platformid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var platformEntity = await _context.Platforms.FindAsync(platformid);
            if (platformEntity is null)
            {
                return NotFound("Platform with this id not found");
            }

            var gameEntity = _context.Games.Include(g => g.Platforms.Where(g => g.Id == platformid)).Where(g => g.Id == gameid).FirstOrDefault();
            if (gameEntity is null)
            {
                return NotFound("Game with this id not found");
            }

            if (!gameEntity.Platforms.Any(g => g.Id == platformid))
            {
                return NotFound("Game with this id doesn't have platform with this id");
            }

            gameEntity.Platforms.Remove(platformEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
