using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRNFinalProject.Data;
using PRNFinalProject.Models;

namespace PRNFinalProject.Logic
{
    public class GenderManagerment
    {
        public List<Genre> GenderList(){
            using (var context=new CenimaDBContext())
            {
                return context.Genres.ToList();
            }
        }

    }
}
