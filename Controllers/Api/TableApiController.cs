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
    public class TableApiController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public TableApiController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: api/TableApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Miza>>> GetMize()
        {
            return await _context.Mize.ToListAsync();
        }

        // GET: api/TableApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Miza>> GetMiza(int id)
        {
            var miza = await _context.Mize.FindAsync(id);

            if (miza == null)
            {
                return NotFound();
            }

            return miza;
        }

        // PUT: api/TableApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMiza(int id, Miza miza)
        {
            if (id != miza.MizaID)
            {
                return BadRequest();
            }

            _context.Entry(miza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MizaExists(id))
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

        // POST: api/TableApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Miza>> PostMiza(Miza miza)
        {
            _context.Mize.Add(miza);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMiza", new { id = miza.MizaID }, miza);
        }

        // DELETE: api/TableApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Miza>> DeleteMiza(int id)
        {
            var miza = await _context.Mize.FindAsync(id);
            if (miza == null)
            {
                return NotFound();
            }

            _context.Mize.Remove(miza);
            await _context.SaveChangesAsync();

            return miza;
        }

        private bool MizaExists(int id)
        {
            return _context.Mize.Any(e => e.MizaID == id);
        }
    }
}
