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
    public class HomeController : Controller
    {
        private readonly RestaurantContext _restaurantContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, RestaurantContext restaurantContext)
        {
            _restaurantContext = restaurantContext;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var context = await _restaurantContext.Restavracije.Include(l => l.Lokacija).ToListAsync();

            ViewData["Drzave"] = new SelectList(await _restaurantContext.GetDistinctCountires());
            ViewData["Kraji"] = new SelectList(await _restaurantContext.GetDistinctCities());

            if (context.Count > 0)
                return View(context[0]);
            else 
                return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Restaurants()
        {
            var context = await _restaurantContext.Restavracije
                .Include(l => l.Lokacija)
                .ToListAsync();
            return View(context);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Search([Bind("Kraj,Drzava")] Lokacija lokacija) 
        {
            ViewData["Drzava"] = lokacija.Drzava;
            ViewData["Kraj"] = lokacija.Kraj;

            string drzava = lokacija.Drzava.ToLower();
            string kraj = lokacija.Kraj.ToLower();

            var context = await _restaurantContext.Restavracije
                .Include(l => l.Lokacija)
                .ToListAsync();
            context = context.Where(r => r.Lokacija != null && r.Lokacija.Drzava.ToLower() == drzava).ToList();
            context = context.Where(r => r.Lokacija != null && r.Lokacija.Kraj.ToLower() == kraj).ToList();
            return View(context);
        }
    }
}
