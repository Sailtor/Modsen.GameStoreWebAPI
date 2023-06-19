using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using DAL.Data;
using BLL.Dtos.OutDto;
using DAL.Models;
using BLL.Dtos.InDto;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly GameStoreDBContext _context;
        private readonly IMapper _mapper;

        public DevelopersController(GameStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Developers
        [Authorize(Roles = "1,2")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Developer>>> GetDevelopers()
        {
            if (_context.Developers == null)
            {
                return NotFound();
            }
            return Ok(await _context.Developers.ToListAsync());
        }

        // GET: api/Developers/5
        [Authorize(Roles = "1,2")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Developer>> GetDeveloper(int id)
        {
            if (_context.Developers == null)
            {
                return NotFound();
            }
            var developer = await _context.Developers.FindAsync(id);

            if (developer == null)
            {
                return NotFound();
            }

            return Ok(developer);
        }

        // PUT: api/Developers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeveloper(int developerid, DeveloperForCreationDto developer)
        {

            var developerEntity = _context.Developers.Find(developerid);
            if (developerEntity is null)
            {
                return NotFound();
            }

            _mapper.Map(developer, developerEntity);
            _context.Set<Developer>().Update(developerEntity);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Developers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<ActionResult<Developer>> PostDeveloper(DeveloperForCreationDto developer)
        {
            if (_context.Developers == null)
            {
                return Problem("Entity set 'GameStoreDBContext.Developers'  is null.");
            }
            var mappedDeveloper = _mapper.Map<Developer>(developer);
            _context.Developers.Add(mappedDeveloper);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDeveloper", new { id = mappedDeveloper.Id }, _mapper.Map<Developer, DeveloperForResponceDto>(mappedDeveloper));
        }

        // DELETE: api/Developers/5
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeveloper(int id)
        {
            if (_context.Developers == null)
            {
                return NotFound();
            }
            var developer = await _context.Developers.FindAsync(id);
            if (developer == null)
            {
                return NotFound();
            }

            _context.Developers.Remove(developer);
            await _context.SaveChangesAsync();

            return Ok(await _context.Developers.ToListAsync());
        }

        private bool DeveloperExists(int id)
        {
            return (_context.Developers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
