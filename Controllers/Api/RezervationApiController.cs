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
    public class RezervationApiController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public RezervationApiController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: api/RezervationApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rezervacija>>> GetRezervacija()
        {
            return await _context.Rezervacija.ToListAsync();
        }

        // GET: api/RezervationApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rezervacija>> GetRezervacija(int id)
        {
            var rezervacija = await _context.Rezervacija.FindAsync(id);

            if (rezervacija == null)
            {
                return NotFound();
            }

            return rezervacija;
        }

        // PUT: api/RezervationApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRezervacija(int id, Rezervacija rezervacija)
        {
            if (id != rezervacija.RezervacijaID)
            {
                return BadRequest();
            }

            _context.Entry(rezervacija).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RezervacijaExists(id))
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

        // POST: api/RezervationApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Rezervacija>> PostRezervacija(Rezervacija rezervacija)
        {
            _context.Rezervacija.Add(rezervacija);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRezervacija", new { id = rezervacija.RezervacijaID }, rezervacija);
        }

        // DELETE: api/RezervationApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rezervacija>> DeleteRezervacija(int id)
        {
            var rezervacija = await _context.Rezervacija.FindAsync(id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            _context.Rezervacija.Remove(rezervacija);
            await _context.SaveChangesAsync();

            return rezervacija;
        }

        private bool RezervacijaExists(int id)
        {
            return _context.Rezervacija.Any(e => e.RezervacijaID == id);
        }
    }
}
