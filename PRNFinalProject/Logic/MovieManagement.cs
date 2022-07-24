using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRNFinalProject.Data;
using PRNFinalProject.Models;

namespace PRNFinalProject.Logic
{
    public class MovieManagement
    {
        public List<Movie> MovieList()
        {
            using (var context = new CenimaDBContext())
            {
                return context.Movies.ToList();
            }
        }
    }
}
