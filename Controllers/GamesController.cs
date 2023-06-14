using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStoreWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using GameStoreWebAPI.Models.Dtos.Out;
using GameStoreWebAPI.Models.Dtos.In;

namespace GameStoreWebAPI.Controllers
{
    [Route("api/games")]
    //[Authorize] //turned off during development 
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

        [HttpGet("{id}")]
        public async Task<ActionResult<GameForResponceDto>> GetGame(int id)
        {
            if (_context.Games == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Game, GameForResponceDto>(game));
        }

        [Authorize (Roles ="1")]
        [HttpPost]
        public async Task<ActionResult<GameForResponceDto>> PostGame(GameForCreationDto game)
        {
            if (_context.Games == null)
            {
                return Problem("Entity set 'GameStoreDBContext.Games'  is null.");
            }
            var mappedGame = _mapper.Map<Game>(game);

            _context.Games.Add(mappedGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = mappedGame.Id }, _mapper.Map<Game, GameForResponceDto>(mappedGame));
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            if (_context.Games == null)
            {
                return NotFound();
            }
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            return (_context.Games?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
