using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rezerviraj.si.Models;
using rezerviraj.si.Data;
using Microsoft.EntityFrameworkCore;

namespace rezerviraj.si.Controllers
{
    public class GuestController : Controller
    {
        private readonly RestaurantContext _context;
        private readonly ILogger<HomeController> _logger;

        public GuestController(ILogger<HomeController> logger, RestaurantContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Guest
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gostje.ToListAsync());
        }

        // GET: Guest/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Guest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ime,Priimek,Email,Geslo,TelefonskaSt")] Gost gost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }

    }
}
