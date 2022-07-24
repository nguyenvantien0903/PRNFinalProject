using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRNFinalProject.Logics;
using PRNFinalProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
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
        private readonly CenimaDBContext c;
        public AdminController(CenimaDBContext c)
        {
            this.c = c;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Login","Security");
            }
            MovieManager movieManagement = new MovieManager();
            ViewBag.Movie = movieManagement.GetAllMovie();
            PersonManager PersonManagement = new PersonManager();
            ViewBag.Person = PersonManagement.GetAllPerson();
            return View();
        }
       // Admin/Rate
        public IActionResult Rate()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Login", "Security");
            }
            MovieManager movieManagement = new MovieManager();
            ViewBag.Movie = movieManagement.GetAllMovie();
            PersonManager PersonManagement = new PersonManager();
            ViewBag.Person = PersonManagement.GetAllPerson();
            RateManager rate = new RateManager();
            ViewBag.rate = rate.GetAllRate();
            return View();
        }
        public IActionResult ListUser()
        {
            //check session
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (HttpContext.Session.GetString("user") != null)
                {
                    HttpContext.Session.Remove("user");
                }
            }
            else
            {
                return RedirectToAction("Login", "Security");
            }
          
            
            ViewData["tgen"] = c.Genres.Count();
            ViewData["tmovie"] = c.Movies.Count();
            ViewData["tuser"] = c.Persons.Count();
            ViewData["trate"] = c.Rates.Count();
            
            ViewData["user"] = c.Persons.Where(p => p.Type == 2).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult ListUser(string statusid)
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Login", "Security");
            }
            string[] n = statusid.Split('-');
            

            Person p1 = c.Persons.FirstOrDefault(p => p.PersonId == Int32.Parse(n[1]));
            
            if (n[0].Equals("Enable"))
            {
                p1.IsActive = true;
                c.Persons.Update(p1);
            }
            else if (n[0].Equals("Disable"))
            {
                p1.IsActive = false;
                c.Persons.Update(p1);
            }
            c.SaveChanges();
            return RedirectToAction("ListUser");
        }
        public IActionResult ListMovie()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Login", "Security");
            }
            
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (HttpContext.Session.GetString("user") != null)
                {
                    HttpContext.Session.Remove("user");
                }
            }
            else
            {
                return RedirectToAction("Login", "Security");
            }
            
            ViewData["tgen"] = c.Genres.Count();
            ViewData["tmovie"] = c.Movies.Count();
            ViewData["tuser"] = c.Persons.Count();
            ViewData["trate"] = c.Rates.Count();
            //
            ViewData["gen"] = c.Genres.ToList();
            ViewData["movie"] = c.Movies.Include(p => p.Genre).Include(p => p.Rates).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult ListMovie(string ids)
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Login", "Security");
            }
            //check session
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (HttpContext.Session.GetString("user") != null)
                {
                    HttpContext.Session.Remove("user");
                }
            }
            else
            {//trả về trang login
                return RedirectToAction("Login", "Home");
            }
            c.Genres.Remove(c.Genres.FirstOrDefault(p => p.GenreId == Int32.Parse(ids)));
            c.SaveChanges();
            return RedirectToAction("ListMovie");
        }
        public IActionResult ListGen()
        {
            //check session
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (HttpContext.Session.GetString("user") != null)
                {
                    HttpContext.Session.Remove("user");
                }
            }
            else
            {//trả về trang login
                return RedirectToAction("Login", "Security");
            }
            //list tổng quan
            ViewData["tgen"] = c.Genres.Count();
            ViewData["tmovie"] = c.Movies.Count();
            ViewData["tuser"] = c.Persons.Count();
            ViewData["trate"] = c.Rates.Count();
            //
            ViewData["gen"] = c.Genres.ToList();
            return View();
        }
        public IActionResult AddGen(string des)
        {
            //check session
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (HttpContext.Session.GetString("user") != null)
                {
                    HttpContext.Session.Remove("user");
                }
            }
            else
            {//trả về trang login
                return RedirectToAction("Login", "Security");
            }
            c.Genres.Add(new Models.Genre { Description = des });
            c.SaveChanges();
            return RedirectToAction("ListGen");
        }
        [HttpPost]
        public IActionResult ListGen(string ids)
        {
            c.Genres.Remove(c.Genres.FirstOrDefault(p => p.GenreId == Int32.Parse(ids)));
            c.SaveChanges();
            return RedirectToAction("ListGen");
        }
        public IActionResult UpdateGen(string id, string des)
        {
            //check session
            if (HttpContext.Session.GetString("admin") != null)
            {
                if (HttpContext.Session.GetString("user") != null)
                {
                    HttpContext.Session.Remove("user");
                }
            }
            else
            {//trả về trang login
                return RedirectToAction("Login", "Security");
            }
            Genre g = c.Genres.FirstOrDefault(p => p.GenreId == Int32.Parse(id));
            g.Description = des;
            c.Genres.Update(g);
            c.SaveChanges();
            return RedirectToAction("ListGen");
        }

        public IActionResult Delete(int Id)
        {
            Movie movie = c.Movies.Include(m => m.Genre).Where(m => m.MovieId == Id).FirstOrDefault();
            return View(movie);
        }

        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            c.Attach(movie);
            c.Entry(movie).State = EntityState.Deleted;
            c.SaveChanges();
            return RedirectToAction("ListMovie");
        }

        public IActionResult Create()
        {
            ViewData["Genres"] = new SelectList(c.Genres, "GenreId", "Description");
            Movie movie = new Movie();
            return View(movie);
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            ViewData["Genres"] = new SelectList(c.Genres, "GenreId", "Description");
            /*var id = c.Movies.Max(m => m.MovieId);
            movie.MovieId = id;*/
            c.Attach(movie);
            c.Entry(movie).State = EntityState.Added;
            c.SaveChanges();
            return RedirectToAction("ListMovie");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            ViewData["Genres"] = new SelectList(c.Genres, "GenreId", "Description");
            Movie movie = c.Movies.Include(m => m.Genre).Where(m => m.MovieId == Id).FirstOrDefault();
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            ViewData["Genres"] = new SelectList(c.Genres, "GenreId", "Description");
            c.Attach(movie);
            c.Entry(movie).State = EntityState.Modified;
            c.SaveChanges();
            return RedirectToAction("ListMovie");
        }

        public IActionResult Details(int Id)
        {
            Movie movie = c.Movies.Include(g=>g.Genre).Where(m => m.MovieId == Id).FirstOrDefault();
            return View(movie);
        }

        public IActionResult CreateGenre()
        {

            Genre genre = new Genre();
            return View(genre);
        }

        [HttpPost]
        public IActionResult CreateGenre(Genre genre)
        {
            c.Attach(genre);
            c.Entry(genre).State = EntityState.Added;
            c.SaveChanges();
            return RedirectToAction("ListGen");
        }

        public IActionResult EditGenre(int Id)
        {
            Genre genre = c.Genres.Where(m => m.GenreId == Id).FirstOrDefault();
            return View(genre);
        }

        [HttpPost]
        public IActionResult EditGenre(Genre genre)
        {
            c.Attach(genre);
            c.Entry(genre).State = EntityState.Modified;
            c.SaveChanges();
            return RedirectToAction("ListGen");
        }
    }
}
