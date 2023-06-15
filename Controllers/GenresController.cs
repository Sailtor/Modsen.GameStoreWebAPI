using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStoreWebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace GameStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly GameStoreDBContext _context;

        public GenresController(GameStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
          if (_context.Genres == null)
          {
              return NotFound();
          }
            return Ok(await _context.Genres.ToListAsync());
        }

        // GET: api/Genres/5
        
        [HttpGet("{id}")]
        
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
          if (_context.Genres == null)
          {
              return NotFound();
          }
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        // PUT: api/Genres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "1")]
        [HttpPut]
        public async Task<IActionResult> PutGenre(Genre genre)
        {
            var dbGenre = await _context.Genres.FindAsync(genre.Id);

            if (dbGenre == null)
                return BadRequest("Genre not found.");

            dbGenre.Name = genre.Name;

            await _context.SaveChangesAsync();

            return Ok(await _context.Genres.ToListAsync());
        }

        // POST: api/Genres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
          if (_context.Genres == null)
          {
              return Problem("Entity set 'GameStoreDBContext.Genres'  is null.");
          }
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return Ok(await _context.Genres.ToListAsync());
        }

        // DELETE: api/Genres/5
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var dbGenre = await _context.Genres.FindAsync(id);
            if (dbGenre == null)
                return BadRequest("Genre not found.");

            _context.Genres.Remove(dbGenre);
            await _context.SaveChangesAsync();

            return Ok(await _context.Genres.ToListAsync());
        }

        private bool GenreExists(int id)
        {
            return (_context.Genres?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
