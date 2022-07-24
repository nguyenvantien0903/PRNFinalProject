using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PRNFinalProject.Data;
using PRNFinalProject.Logics;
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

        public IActionResult Index(int Id, int Page)
        {
          
            GenreManager genreManager = new GenreManager();
            ViewBag.Genres = genreManager.GetAllGenres();
        


            MovieManager movieManager = new MovieManager();
            List<Movie> movies = movieManager.GetAllMovie(Id);
            /*List<Movie> movies = movieManager.GetMoives(Id, (Page - 1) * PageSize + 1, PageSize);*/


            Dictionary<int, double> listr = new Dictionary<int, double>();
            RateManager rateManager = new RateManager();
            foreach (Movie m in movies)
            {
                List<Rate> rate = rateManager.GetRateByMovieId(m.MovieId);
                double total = 0;
                foreach (Rate item in rate)
                {
                    total += (double)item.NumericRating;
                }
                double NumericRating = total / rate.Count;
                listr.Add(m.MovieId, NumericRating);
            }
            ViewBag.rates = listr;


            //lay cac du lieu de hien thi dc thanh pager
            /*int TotalMovie = movieManager.GetNumberOfMovies(Id);
            int TotalPage = TotalMovie / PageSize;
            if (TotalMovie % PageSize != 0) TotalPage++;
            ViewData["TotalPage"] = TotalPage;
            ViewData["CurrenPage"] = Page;
            ViewData["CurrentGenre"] = Id;*/
            return View(movies);
        }

        public IActionResult SearchMovie()
        {
            //lay danh sach tat ca cac genres
            GenreManager genreManager = new GenreManager();
            ViewBag.Genres = genreManager.GetAllGenres();
            //lay danh sacsh theo genre


            MovieManager movieManager = new MovieManager();
            string search = Request.Form["search"];
            List<Movie> movies = movieManager.SearchMovieByName(search);
            /*List<Movie> movies = movieManager.GetMoives(Id, (Page - 1) * PageSize + 1, PageSize);*/


            Dictionary<int, double> listr = new Dictionary<int, double>();
            RateManager rateManager = new RateManager();
            foreach (Movie m in movies)
            {
                List<Rate> rate = rateManager.GetRateByMovieId(m.MovieId);
                double total = 0;
                foreach (Rate item in rate)
                {
                    total += (double)item.NumericRating;
                }
                double NumericRating = total / rate.Count;
                listr.Add(m.MovieId, NumericRating);
            }
            ViewBag.rates = listr;

            return View("Index", movies);
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

    }
}