using Microsoft.EntityFrameworkCore;
using PRNFinalProject.Data;
using PRNFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRNFinalProject.Logics
{
    public class MovieManager
    {
        public List<Movie> GetAllMovie(int GenreID)
        {
            using (var context = new CenimaDBContext())
            {
                if (GenreID == 0)
                {
                    return context.Movies.Include(g => g.Genre).ToList();
                }
                else
                {
                    return context.Movies.Include(g => g.Genre).Where(x => x.GenreId == GenreID).ToList();
                }
            }
        }

        //GetMovie by GenreId
        public List<Movie> GetMoives(int GenreID, int Offset, int Count)
        {
            using (var context = new CenimaDBContext())
            {
                if (GenreID == 0)
                {
                    return context.Movies.Include(g =>g.Genre).Skip(Offset - 1).Take(Count).ToList();
                }
                else
                {
                    return context.Movies.Include(g => g.Genre).Where(x => x.GenreId == GenreID).Skip(Offset - 1).Take(Count).ToList();
                }
            }
        }
        public int GetNumberOfMovies(int GenreID)
        {
            using (var context = new CenimaDBContext())
            {
                if (GenreID == 0)
                {
                    return context.Movies.Count();
                }
                else
                {
                    return context.Movies.Where(x => x.GenreId == GenreID).Count();
                }
            }
        }

        public Movie GetMoivesById(int ID)
        {
            using (var context = new CenimaDBContext())
            {
                    return context.Movies.Include(x=>x.Genre).Where(m => m.MovieId == ID).FirstOrDefault();
            }
        }

    }
}

