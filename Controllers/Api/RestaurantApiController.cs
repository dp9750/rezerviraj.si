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
    public class RestaurantApiController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public RestaurantApiController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: api/RestaurantApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restavracija>>> GetRestavracije()
        {
            return await _context.Restavracije.ToListAsync();
        }

        // GET: api/RestaurantApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restavracija>> GetRestavracija(string id)
        {
            var restavracija = await _context.Restavracije.FindAsync(id);

            if (restavracija == null)
            {
                return NotFound();
            }

            return restavracija;
        }

        // PUT: api/RestaurantApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestavracija(string id, Restavracija restavracija)
        {
            if (id != restavracija.Id)
            {
                return BadRequest();
            }

            _context.Entry(restavracija).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestavracijaExists(id))
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

        // POST: api/RestaurantApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Restavracija>> PostRestavracija(Restavracija restavracija)
        {
            _context.Restavracije.Add(restavracija);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RestavracijaExists(restavracija.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRestavracija", new { id = restavracija.Id }, restavracija);
        }

        // DELETE: api/RestaurantApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Restavracija>> DeleteRestavracija(string id)
        {
            var restavracija = await _context.Restavracije.FindAsync(id);
            if (restavracija == null)
            {
                return NotFound();
            }

            _context.Restavracije.Remove(restavracija);
            await _context.SaveChangesAsync();

            return restavracija;
        }

        private bool RestavracijaExists(string id)
        {
            return _context.Restavracije.Any(e => e.Id == id);
        }
    }
}
