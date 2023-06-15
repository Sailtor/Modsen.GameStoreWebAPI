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
    [Route("api/genres")]
    //[Authorize]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly GameStoreDBContext _context;
        private readonly IMapper _mapper;

        public GenresController(GameStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreForResponceDto>>> GetGenres()
        {
          if (_context.Genres == null)
          {
              return NotFound();
          }
            return Ok(_mapper.Map<List<Genre>, List<GenreForResponceDto>>(await _context.Genres.ToListAsync()));
        }

        // GET: api/Genres/5
        
        [HttpGet("{genreid}")]
        
        public async Task<ActionResult<GenreForResponceDto>> GetGenre(int genreid)
        {
          if (_context.Genres == null)
          {
              return NotFound();
          }
            var genre = await _context.Genres.FindAsync(genreid);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Genre, GenreForResponceDto>(genre));
        }

        // PUT: api/Genres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "1")]
        [HttpPut("{genreid}")]
        public async Task<IActionResult> PutGenre(int genreid,GenreForCreationDto genre)
        {
            if (genre is null)
            {
                return BadRequest("Genre object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var genreEntity = _context.Genres.Find(genreid);
            if (genreEntity is null)
            {
                return NotFound();
            }

            _mapper.Map(genre, genreEntity);
            _context.Set<Genre>().Update(genreEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Genres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<ActionResult<GenreForResponceDto>> PostGenre(GenreForCreationDto genre)
        {
            if (genre is null)
            {
                return BadRequest("Genre object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var mappedGenre = _mapper.Map<Genre>(genre);

            _context.Genres.Add(mappedGenre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenre", new { id = mappedGenre.Id }, _mapper.Map<Genre, GenreForResponceDto>(mappedGenre));
        }

        // DELETE: api/Genres/5
        [Authorize(Roles = "1")]
        [HttpDelete("{genreid}")]
        public async Task<IActionResult> DeleteGenre(int genreid)
        {
            var dbGenre = await _context.Genres.FindAsync(genreid);
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
