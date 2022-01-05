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
        private readonly RestaurantContext _restaurantContext;
        private readonly ILogger<HomeController> _logger;

        public RezervationController(ILogger<HomeController> logger, RestaurantContext restaurantContext)
        {
            _restaurantContext = restaurantContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var context = await _restaurantContext.Restavracije.Include(l => l.Lokacija).ToListAsync();

            //ViewData["Drzave"] = new SelectList(await _restaurantContext.GetDistinctCountires());
            //ViewData["Kraji"] = new SelectList(await _restaurantContext.GetDistinctCities());

            return View(null);

            //return View(context[0]);
        }

        public async Task<IActionResult> Create()
        {
            var restavracije = await _restaurantContext.Restavracije.ToListAsync();

            ViewData["Restavracije"] = new SelectList(restavracije);

            return View();
        }
    }
}
