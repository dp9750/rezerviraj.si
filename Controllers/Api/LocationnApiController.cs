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
    public class LocationnApiController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public LocationnApiController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: api/LocationnApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lokacija>>> GetLokacija()
        {
            return await _context.Lokacija.ToListAsync();
        }

        // GET: api/LocationnApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lokacija>> GetLokacija(int id)
        {
            var lokacija = await _context.Lokacija.FindAsync(id);

            if (lokacija == null)
            {
                return NotFound();
            }

            return lokacija;
        }

        // PUT: api/LocationnApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLokacija(int id, Lokacija lokacija)
        {
            if (id != lokacija.LokacijaID)
            {
                return BadRequest();
            }

            _context.Entry(lokacija).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LokacijaExists(id))
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

        // POST: api/LocationnApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Lokacija>> PostLokacija(Lokacija lokacija)
        {
            _context.Lokacija.Add(lokacija);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLokacija", new { id = lokacija.LokacijaID }, lokacija);
        }

        // DELETE: api/LocationnApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lokacija>> DeleteLokacija(int id)
        {
            var lokacija = await _context.Lokacija.FindAsync(id);
            if (lokacija == null)
            {
                return NotFound();
            }

            _context.Lokacija.Remove(lokacija);
            await _context.SaveChangesAsync();

            return lokacija;
        }

        private bool LokacijaExists(int id)
        {
            return _context.Lokacija.Any(e => e.LokacijaID == id);
        }
    }
}
