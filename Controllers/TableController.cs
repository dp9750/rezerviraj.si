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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

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
            return View(context);
        }

        // GET: Table/Create
        public IActionResult Create() {
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
                return RedirectToAction(nameof(Index));
            }
            
            return View(miza);
        }
    }
}
