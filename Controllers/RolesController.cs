using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStoreWebAPI.Models;
using GameStoreWebAPI.Models.Dtos.In;
using AutoMapper;
using GameStoreWebAPI.Models.Dtos.Out;
using Microsoft.AspNetCore.Authorization;

namespace GameStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly GameStoreDBContext _context;
        private readonly IMapper _mapper;

        public RolesController(GameStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Roles
        [Authorize(Roles = "1")]
        [HttpGet]
        public async Task<ActionResult<RoleForResponceDto>> GetRoles()
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            return Ok(await _context.Roles.ToListAsync());
        }

        // GET: api/Roles/5
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleForResponceDto>> GetRole(int id)
        {
          if (_context.Roles == null)
          {
              return NotFound();
          }
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int roleid, RoleForCreationDto roles)
        {

            var roleEntity = _context.Roles.Find(roleid);
            if (roleEntity is null)
            {
                return NotFound();
            }

            _mapper.Map(roles, roleEntity);
            _context.Set<Role>().Update(roleEntity);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(RoleForCreationDto role)
        {
            if (_context.Roles == null)
            {
                return Problem("Entity set 'GameStoreDBContext.Roles'  is null.");
            }
            var mappedRoles = _mapper.Map<Role>(role);
            _context.Roles.Add(mappedRoles);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRoles", new { id = mappedRoles.Id }, _mapper.Map<Role, RoleForResponceDto>(mappedRoles));
        }

        // DELETE: api/Roles/5
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (_context.Roles == null)
            {
                return NotFound();
            }
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok(await _context.Roles.ToListAsync());
        }

        private bool RoleExists(int id)
        {
            return (_context.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
