using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRNFinalProject.Data;
using PRNFinalProject.Models;
namespace PRNFinalProject.Logic
{
    public class RateManagement
    {
        public List<Rate> RateList()
        {
            using (var context = new CenimaDBContext())
            {
                return context.Rates.ToList();
            }
        }
        public List<Rate> RateListByMovieId(int id)
        {
            using (var context = new CenimaDBContext())
            {
                return context.Rates.Where(p=>p.MovieId==id).ToList();
            }
        }
    }
}
