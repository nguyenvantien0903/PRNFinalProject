﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PRNFinalProject.Logic;
using PRNFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PRNFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            MovieManagement movieManagement = new MovieManagement();
            ViewBag.Movie = movieManagement.MovieList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ListMovie()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
