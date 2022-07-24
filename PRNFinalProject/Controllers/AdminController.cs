using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRNFinalProject.Data;
using PRNFinalProject.Logics;
using PRNFinalProject.Models;

namespace PRNFinalProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly CenimaDBContext context;

        public AdminController(CenimaDBContext _context)
        {
            context = _context;
        }

        public IActionResult Admin()
        {
            return View();
        }

        // GET: Admin
        public IActionResult ListUser()
        {
            List<Person> person = context.Persons.ToList();
            return View(person);
        }

        [HttpPost]
        public IActionResult ListUser(bool statusid, int Id)
        {
            PersonManager personManager = new PersonManager();
            Person person = personManager.GetPersonByMovieId(Id);

            if (statusid != true)
            {
                person.IsActive = false;
                context.Persons.Update(person);
                context.SaveChanges();
            }
            else {
                person.IsActive = true;
                context.Persons.Update(person);
                context.SaveChanges();
            }
            return RedirectToAction("ListUser", "Admin");


        }

        public IActionResult ListMovie()
        {
            MovieManager movieManager = new MovieManager();
            List <Movie> movies = movieManager.GetAllMovie();
            return View(movies);
        }

        [HttpGet]
        public IActionResult AddMoive()
        {
            ViewData["Genres"] = new SelectList(context.Genres, "GenreId", "Description");
            Movie movie = new Movie();
            return View(movie);
        }

        [HttpPost]
        public IActionResult AddMoive(Movie movie)
        {
            ViewData["Genres"] = new SelectList(context.Genres, "GenreId", "Description");
            var id = context.Movies.Max(m => m.MovieId);
            movie.MovieId = id;
            context.Attach(movie);
            context.Entry(movie).State = EntityState.Added;
            context.SaveChanges();
            return RedirectToAction("ListMovie", "Admin");
        }

        public IActionResult DeleteMovie(int id)
        {
            MovieManager movieManager = new MovieManager();
            movieManager.DeleteMovie(id);
            RateManager.DeleteRate(id);
            return RedirectToAction("ListMovie", "Admin");
        }

        public IActionResult EditMovie(int Id)
        {
            MovieManager movieManager = new MovieManager();
            Movie moive = movieManager.GetMoivesById(Id);
            ViewData["Genres"] = new SelectList(context.Genres, "GenreId", "Description");
            return View(moive);
        }

        [HttpPost]
        public IActionResult EditMovie(Movie movie)
        {
                ViewData["Genres"] = new SelectList(context.Genres, "GenreId", "Description");
                context.Attach(movie);
                context.Entry(movie).State = EntityState.Modified;
                context.SaveChanges();
            return RedirectToAction("ListMovie", "Admin");
        }

        public IActionResult ListGenre()
        {
            GenreManager genreManager = new GenreManager();
            List<Genre> genres = genreManager.GetAllGenres();
            return View(genres);
        }


        public IActionResult AddGenre()
        {
            Genre genre = new Genre();
            return View(genre);
        }

        [HttpPost]
        public IActionResult AddGenre(String description)
        {

            context.Genres.Add(new Models.Genre{ Description = description });
            context.SaveChanges();
            return RedirectToAction("ListGenre", "Admin");
        }

        public IActionResult EditGenre(int id)
        {
            GenreManager genreManager = new GenreManager();
            Genre genre =  genreManager.GetGenresById(id);
            return View(genre);
        }

        [HttpPost]
        public IActionResult EditGenre(string id, string description)
        {
            Genre genre = context.Genres.FirstOrDefault(g => g.GenreId == Int32.Parse(id));
            genre.Description = description;
            context.Genres.Update(genre);
            context.SaveChanges();
            return RedirectToAction("ListGenre", "Admin");
        }
    }
}
