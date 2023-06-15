using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStoreWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace GameStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly GameStoreDBContext _context;

        public PlatformsController(GameStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Platforms
        [HttpGet]
        public async Task<ActionResult<List<Platform>>> GetPlatforms()
        {
            if (_context.Platforms == null)
            {
                return NotFound();
            }
            return Ok(await _context.Platforms.ToListAsync());
        }

        // GET: api/Platforms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Platform>> GetPlatform(int id)
        {
            if (_context.Platforms == null)
            {
                return NotFound();
            }
            var platform = await _context.Platforms.FindAsync(id);

            if (platform == null)
            {
                return NotFound();
            }

            return platform;
        }

        // PUT: api/Platforms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "1")]
        [HttpPut]
        public async Task<IActionResult> PutPlatform(Platform platform)
        {
            var dbPlatform = await _context.Platforms.FindAsync(platform.Id);


             if (dbPlatform == null)
                return BadRequest("Platform not found.");

            dbPlatform.Name = platform.Name;

            await _context.SaveChangesAsync();

            return Ok(await _context.Platforms.ToListAsync());
        }



        // POST: api/Platforms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<ActionResult<List<Platform>>> PostPlatform(Platform platform)
        {
            if (_context.Platforms == null)
            {
                return Problem("Entity set 'GameStoreDBContext.Platforms'  is null.");
            }
            _context.Platforms.Add(platform);
            await _context.SaveChangesAsync();

            return Ok(await _context.Platforms.ToListAsync());
        }

        // DELETE: api/Platforms/5
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatform(int id)
        {
            var dbPlatform = await _context.Platforms.FindAsync(id);
            if (dbPlatform == null)
                return BadRequest("Platform not found.");

            _context.Platforms.Remove(dbPlatform);
            await _context.SaveChangesAsync();

            return Ok(await _context.Platforms.ToListAsync());
        }
    }
}