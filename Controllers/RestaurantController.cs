using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rezerviraj.si.Models;
using rezerviraj.si.Data;

namespace rezerviraj.si.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly RestaurantContext _context;

        public RestaurantController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: Restaurant
        public async Task<IActionResult> Index()
        {
            return View(await _context.Restavracije.ToListAsync());
        }

        // GET: Restaurant/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var restavracija = await _context.Restavracije
                .Include(l => l.Lokacija)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (restavracija == null)
                return NotFound();

            return View(restavracija);
        }

        // GET: Restaurant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestavracijaID,Email,Geslo,Naziv,DatumRegistracije,TelefonskaSt")] Restavracija restavracija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restavracija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restavracija);
        }

        // GET: Restaurant/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var restavracija = await _context.Restavracije.FindAsync(id);

            if (restavracija == null) return NotFound();

            return View(restavracija);
        }

        // POST: Restaurant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,RestavracijaID,Email,Geslo,Naziv,DatumRegistracije,TelefonskaSt")] Restavracija restavracija)
        {
            if (id != restavracija.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try {
                    _context.Update(restavracija);
                    await _context.SaveChangesAsync();
                } 
                catch (DbUpdateConcurrencyException) {
                    if (!RestavracijaExists(restavracija.Id))
                        return NotFound();
                    else 
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(restavracija);
        }

        // GET: Restaurant/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var restavracija = await _context.Restavracije
                .Include(l => l.Lokacija)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (restavracija == null) return NotFound();

            return View(restavracija);
        }

        // POST: Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var restavracija = await _context.Restavracije.FindAsync(id);
            _context.Restavracije.Remove(restavracija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestavracijaExists(string id)
        {
            return _context.Restavracije.Any(e => e.Id == id);
        }
    }
}
