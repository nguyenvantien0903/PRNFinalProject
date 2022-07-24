using Microsoft.EntityFrameworkCore;
using PRNFinalProject.Data;
using PRNFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRNFinalProject.Logics
{
    public class RateManager
    {
        public List<Rate> GetAllRate()
        {
            using (var context = new CenimaDBContext())
            {
                return context.Rates.ToList();
            }
        }

        public List<Rate> GetRateByMovieId(int id)
        {
            using (var context = new CenimaDBContext())
            {
                return context.Rates.Include(p => p.Person).Where(m => m.MovieId == id).ToList();
            }
        }

        internal static void DeleteRate(int id)
        {
            List<Rate> rates = new List<Rate>();
            using (var context = new CenimaDBContext())
            {
                rates = context.Rates.Where(x => x.MovieId == id).ToList();
                if (rates.Count > 0)
                {
                    context.Rates.RemoveRange(rates);

                }
                context.SaveChanges();
            }
        }
    }
}
