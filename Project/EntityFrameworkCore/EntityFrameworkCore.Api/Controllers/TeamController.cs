using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.Data;
using EntityFrameworkCore.Domain;
using EntityFrameworkCore.Api.DTO;

namespace EntityFrameworkCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly FootballLeagueDbContext _context;

        public TeamController(FootballLeagueDbContext context)
        {
            _context = context;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeam()
        {
            // return await _context.Team.ToListAsync();

            var teams = await _context.Team
                .Select(team => new TeamDTO
                {
                    Id = team.Id,
                    Name = team.Name,
                    CoachName = team.Coach.Name
                })
                .ToListAsync();

            return teams;
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {



            // var team = await _context.Team.FindAsync(id);

            var team = await _context.Team
                .Include(team => team.Coach)  // Adding related Coach Entries
                .Include(team => team.League) // Adding related League Entries
                .FirstOrDefaultAsync(team => team.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Team/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // POST: api/Team
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            _context.Team.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.Id }, team);
        }

        // DELETE: api/Team/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            // var team = await _context.Team.FindAsync(id);
            // if (team == null)
            // {
            //     return NotFound();
            // }

            // _context.Team.Remove(team);
            // await _context.SaveChangesAsync();

            await _context.Team.Where(q => q.Id == id).ExecuteDeleteAsync();

            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return _context.Team.Any(e => e.Id == id);
        }
    }
}
