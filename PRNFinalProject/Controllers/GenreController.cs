using Microsoft.AspNetCore.Mvc;
using PRNFinalProject.Data;
using PRNFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRNFinalProject.Controllers
{
    public class GenreController : Controller
    {
        private readonly CenimaDBContext _context;
        public GenreController(CenimaDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Genre> genres = _context.Genres.ToList();
            return View(genres);
        }
    }
}
