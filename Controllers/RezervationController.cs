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
        private string _resID = null;

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
        public IActionResult Create()
        {
            return View();
        }

        // GET: Rezervation/Login
        public IActionResult Login() {
            return View();
        }
    }
}
