using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rezerviraj.si.Models;
using rezerviraj.si.Data;
using Microsoft.EntityFrameworkCore;

namespace rezerviraj.si.Controllers
{
    public class TableController : Controller
    {
        private readonly RestaurantContext _restaurantContext;
        private readonly ILogger<HomeController> _logger;

        public TableController(ILogger<HomeController> logger, RestaurantContext restaurantContext)
        {
            _restaurantContext = restaurantContext;
            _logger = logger;
        }

        // GET: Table/RestaurantID
        public async Task<IActionResult> Index(string id)
        {
            var context = await _restaurantContext.Mize
                .Where(m => m.RestavracijaID == id)
                .ToListAsync();

            var restaurantName = await _restaurantContext.Restavracije
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurantName == null) return NotFound();
            ViewData["Naziv"] = restaurantName.Naziv;

            return View(context);
        }

        // GET: Table/Create/RestavracijaID
        public async Task<IActionResult> Create(string id) {
            var restaurantName = await _restaurantContext.Restavracije
                .FirstOrDefaultAsync(r => r.Id == id);

            ViewData["Naziv"] = restaurantName.Naziv;
            ViewData["RestavracijaID"] = id;

            return View();
        }

        // POST: Table/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestavracijaID,MizaID,StMize,StOseb")] Miza miza)
        {
            if (ModelState.IsValid)
            {
                _restaurantContext.Add(miza);
                await _restaurantContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Table", new { id = miza.RestavracijaID });
            }
            
            return View(miza);
        }

        // GET: Table/Delete/MizaID
        public async Task<IActionResult> Delete(int id) {
            var miza = await _restaurantContext.Mize
                .FirstOrDefaultAsync(m => m.MizaID == id);

            if (miza == null) 
                return NotFound();
            return View(miza);
        }

        // POST: Table/Delete/MizaID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miza = await _restaurantContext.Mize.FindAsync(id);
            _restaurantContext.Mize.Remove(miza);
            await _restaurantContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Table", new { id = miza.RestavracijaID });
        }
    }
}
