using PRNFinalProject.Data;
using PRNFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRNFinalProject.Logics
{
    public class PersonManager
    {
        public Person GetPersonByMovieId(int ID)
        {
            using (var context = new CenimaDBContext())
            {
                return context.Persons.Where(m => m.PersonId == ID).FirstOrDefault();
            }
        }
    }
}
