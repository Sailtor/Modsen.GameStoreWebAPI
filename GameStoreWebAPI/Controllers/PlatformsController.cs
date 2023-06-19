using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using AutoMapper;
using DAL.Data;
using BLL.Dtos.OutDto;
using BLL.Dtos.InDto;
using DAL.Models;

namespace API.Controllers
{
    [Route("api/platforms")]
    [Authorize]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly GameStoreDBContext _context;
        private readonly IMapper _mapper;

        public PlatformsController(GameStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Platforms
        [HttpGet]
        public async Task<ActionResult<List<Platform>>> GetPlatforms()
        {
            if (_context.Platforms == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<Platform>, List<PlatformForResponceDto>>(await _context.Platforms.ToListAsync()));
        }

        // GET: api/Platforms/5
        [HttpGet("{platformid}")]
        public async Task<ActionResult<PlatformForResponceDto>> GetPlatform(int platformid)
        {
            if (_context.Platforms == null)
            {
                return NotFound();
            }

            var platform = await _context.Platforms.FindAsync(platformid);

            if (platform == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Platform, PlatformForResponceDto>(platform));
        }

        // PUT: api/Platforms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "1")]
        [HttpPut("{platformid}")]
        public async Task<IActionResult> PutPlatform(int platformid, PlatformForCreationDto platform)
        {
            if (platform is null)
            {
                return BadRequest("Platform object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var platformEntity = _context.Platforms.Find(platformid);
            if (platformEntity is null)
            {
                return NotFound();
            }

            _mapper.Map(platform, platformEntity);
            _context.Set<Platform>().Update(platformEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        // POST: api/Platforms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<ActionResult<List<PlatformForResponceDto>>> PostPlatform(PlatformForCreationDto platform)
        {
            if (platform is null)
            {
                return BadRequest("Platform object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var mappedPlatform = _mapper.Map<Platform>(platform);

            _context.Platforms.Add(mappedPlatform);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlatform", new { id = mappedPlatform.Id }, _mapper.Map<Platform, PlatformForResponceDto>(mappedPlatform));
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

            return NoContent();
        }
    }
}