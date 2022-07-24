using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRNFinalProject.Data;
using PRNFinalProject.Logics;
using PRNFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRNFinalProject.Controllers
{
    public class DetailController : Controller
    {

        private readonly CenimaDBContext context;

        public DetailController(CenimaDBContext _context)
        {
            context = _context;
        }
        public IActionResult DetailMovie(int Id)
        {
            MovieManager movieManager = new MovieManager();
            Movie movies = movieManager.GetMoivesById(Id);


            //GenreManager genreManager = new GenreManager();
            //ViewBag.Genres = genreManager.GetGenresById((int)movies.GenreId);

            RateManager rateManager = new RateManager();
            List<Rate> rate = rateManager.GetRateByMovieId(Id);
            double total = 0;
            foreach (Rate item in rate)
            {
                total = (double)item.NumericRating;
            }
            double NumericRating = total / rate.Count;
            ViewData["TotalPoits"] = NumericRating;


            ViewBag.Rates = rateManager.GetRateByMovieId(Id);
            return View(movies);
        }

        public IActionResult AddComment(int Id, float points, string comment)
        {
            string user = HttpContext.Session.GetString("account");
            if (user != null)
            {
                Person person = JsonConvert.DeserializeObject<Person>(user);
                Rate rates = context.Rates.FirstOrDefault(x => x.PersonId.ToString().Equals(user) && x.MovieId == Id);

                rates.PersonId = person.PersonId;
                rates.MovieId = Id;
                rates.NumericRating = points;
                rates.Comment = comment;
                rates.Time = DateTime.Now;
                context.Rates.Update(rates);
            }
            context.SaveChanges();

            return RedirectToAction("DetailMovie", "Detail", new { Id = Id });  


        }
    }
}
