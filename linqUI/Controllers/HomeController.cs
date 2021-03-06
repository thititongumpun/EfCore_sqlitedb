﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using linqUI.Models;
using linqUI.Data;
using Microsoft.EntityFrameworkCore;

namespace linqUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Northwind _db;

        public HomeController(ILogger<HomeController> logger, Northwind db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel
            {
                VisitorCount = (new Random().Next(1, 1001)),
                Categories = _db.Categories.ToList(),
                Products = _db.Products.ToList()
            };
            return View(model);
        }
            
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ProductsMoreCost(decimal? price)
        {
            if (!price.HasValue)
            {
                return NotFound();
            }
            IEnumerable<Product> model = _db.Products.Include(p => p.Category)
                                                            .Include(p => p.Supplier);
            if (model.Count() == 0)
            {
                return NotFound($"not found {price:C}");
            }
            return View(model);
        }
    }
}
