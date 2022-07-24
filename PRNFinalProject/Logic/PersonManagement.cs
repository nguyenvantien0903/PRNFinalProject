using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRNFinalProject.Data;
using PRNFinalProject.Models;
namespace PRNFinalProject.Logic
{
    public class PersonManagement
    {
        public List<Person> PersonList()
        {
            using (var context = new CenimaDBContext())
            {
                return context.Persons.ToList();
               
            }
        }
    }
}
