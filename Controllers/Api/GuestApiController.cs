using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rezerviraj.si.Data;
using rezerviraj.si.Models;

namespace rezerviraj.si.Controllers_Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestApiController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public GuestApiController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: api/GuestApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gost>>> GetGostje()
        {
            return await _context.Gostje.ToListAsync();
        }

        // GET: api/GuestApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gost>> GetGost(int id)
        {
            var gost = await _context.Gostje.FindAsync(id);

            if (gost == null)
            {
                return NotFound();
            }

            return gost;
        }

        // PUT: api/GuestApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGost(int id, Gost gost)
        {
            if (id != gost.GostID)
            {
                return BadRequest();
            }

            _context.Entry(gost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GostExists(id))
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

        // POST: api/GuestApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Gost>> PostGost(Gost gost)
        {
            _context.Gostje.Add(gost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGost", new { id = gost.GostID }, gost);
        }

        // DELETE: api/GuestApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gost>> DeleteGost(int id)
        {
            var gost = await _context.Gostje.FindAsync(id);
            if (gost == null)
            {
                return NotFound();
            }

            _context.Gostje.Remove(gost);
            await _context.SaveChangesAsync();

            return gost;
        }

        private bool GostExists(int id)
        {
            return _context.Gostje.Any(e => e.GostID == id);
        }
    }
}
