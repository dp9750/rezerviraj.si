using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rezerviraj.si.Models;
using rezerviraj.si.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace rezerviraj.si.Controllers
{
    public class RezervationController : Controller
    {
        private readonly RestaurantContext _context;
        private readonly ILogger<HomeController> _logger;

        public RezervationController(ILogger<HomeController> logger, RestaurantContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Rezervation
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rezervacija.ToListAsync());
        }

        // GET: Rezervation/Create
        public async Task<IActionResult> Create(string RestavracijaID, string GostID)
        {
            RezervationRequest request = new RezervationRequest {
                RezerviranoZa = DateTime.Now,
                StOseb = 0,
                Restavracija = await _context.Restavracije.FirstOrDefaultAsync(r => r.Id == RestavracijaID),
                GostID = GostID,
                RestavracijaID = RestavracijaID,
                Gost = await _context.Gostje.FirstOrDefaultAsync(g => g.GostID == int.Parse(GostID))
            };

            return View(request);
        }

        // POST: Rezervation/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("GostID,RestavracijaID,RezerviranoZa,StOseb")] RezervationRequest request)
        {
            Rezervacija rezervacija = new Rezervacija {
                Restavracija = await _context.Restavracije.FirstOrDefaultAsync(r => r.Id == request.RestavracijaID),
                Gost = await _context.Gostje.FirstOrDefaultAsync(g => g.GostID == int.Parse(request.GostID)),
                RezerviranoZa = request.RezerviranoZa,
                DatumRezervacije = DateTime.Now,
                StOseb = request.StOseb
            };

            if (ModelState.IsValid)
            {
                _context.Add(rezervacija);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            return View(request);
        }

        // GET: Rezervation/Login/RestaurantID
        public IActionResult Login(string id) {
            return View(new RezervationRequest { RestavracijaID = id });
        }

        // POST: Rezervation/Login
        [HttpPost]
        public async Task<IActionResult> Login([Bind("RestavracijaID,Email,Geslo")] RezervationRequest request)
        {
            var gost = await _context.Gostje
                .FirstOrDefaultAsync(g => g.Email == request.Email && g.Geslo == request.Geslo);

            if (gost == null) return NotFound();


            return RedirectToAction(
                "Create", 
                "Rezervation", 
                new { RestavracijaID = request.RestavracijaID, GostID = gost.GostID }
            );
        }
    }
}
