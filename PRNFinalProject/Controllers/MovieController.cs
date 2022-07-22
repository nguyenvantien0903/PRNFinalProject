using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRNFinalProject.Data;
using PRNFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRNFinalProject.Controllers
{
    public class MovieController : Controller
    {
        private readonly CenimaDBContext _context;

        public MovieController(CenimaDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Movie> movies = _context.Movies.Include(m => m.Genre).Include(m => m.Rates).ToList();
            return View(movies);
        }

        public IActionResult Details(int Id)
        {
            Movie movie = _context.Movies.Where(m => m.MovieId == Id).FirstOrDefault();
            return View(movie);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            ViewData["Genres"] = new SelectList(_context.Genres, "GenreId", "Description");
            Movie movie = _context.Movies.Include(m => m.Genre).Where(m => m.MovieId == Id).FirstOrDefault();
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            ViewData["Genres"] = new SelectList(_context.Genres, "GenreId", "Description");
            _context.Attach(movie);
            _context.Entry(movie).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            Movie movie = _context.Movies.Include(m => m.Genre).Where(m => m.MovieId == Id).FirstOrDefault();
            return View(movie);
        }

        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            _context.Attach(movie);
            _context.Entry(movie).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            ViewData["Genres"] = new SelectList(_context.Genres, "GenreId", "Description");
            Movie movie = new Movie();
            return View(movie);
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            ViewData["Genres"] = new SelectList(_context.Genres, "GenreId", "Description");
            var id = _context.Movies.Max(m => m.MovieId);
            movie.MovieId = id;
            _context.Attach(movie);
            _context.Entry(movie).State = EntityState.Added;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
