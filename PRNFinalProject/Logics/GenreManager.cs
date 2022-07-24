using PRNFinalProject.Data;
using PRNFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRNFinalProject.Logics
{
    public class GenreManager
    {
        public List<Genre> GetAllGenres()
        {
            using (var context = new CenimaDBContext())
            {
                return context.Genres.ToList();
            }
        }
        public Genre GetGenresById(int id)
        {
            using (var context = new CenimaDBContext())
            {
                return context.Genres.Where(g => g.GenreId == id).FirstOrDefault();
            }
        }
    }
}
