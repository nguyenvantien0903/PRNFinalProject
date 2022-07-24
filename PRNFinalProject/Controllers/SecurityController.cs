using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRNFinalProject.Data;
using PRNFinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRNFinalProject.Controllers
{
    public class SecurityController : Controller
    {
        private readonly CenimaDBContext context;

        public SecurityController(CenimaDBContext _context)
        {
            context = _context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Person person)
        {
            var checklogin = context.Persons.Where(x => x.Email.Equals(person.Email) && x.Password.Equals(person.Password)).FirstOrDefault();
            if (checklogin != null)
            {
            Person account = context.Persons.FirstOrDefault(x => x.Email.Equals(person.Email) && x.Password.Equals(person.Password));
            if (account.IsActive == true)
            {
                    if (account.Type == 1)
                    {
                        HttpContext.Session.SetString("account", JsonConvert.SerializeObject(account));
                        return RedirectToAction("Admin", "Admin");
                    }
                    else
                    {
                        HttpContext.Session.SetString("account", JsonConvert.SerializeObject(account));
                        return RedirectToAction("Index", "Home");
                    }

            }
            else
            {
                ViewBag.Notification = "Your Account is Block";
                return View();
            }
            }
            else
            {
                ViewBag.Notification = "Wrong Email or password";
                return View();
            }
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Person person)
        {
            if(context.Persons.Any(x => x.Email == person.Email))
            {
                ViewBag.Notification = "This account has already existed";
                return View();
            }
            else
            {
                person.IsActive = true;
                person.Type = 2;    
                context.Persons.Add(person);
                context.SaveChanges();

                Person account = context.Persons.FirstOrDefault(x => x.Email.Equals(person.Email) && x.Password.Equals(person.Password));
                //HttpContext.Session.SetString("account", JsonConvert.SerializeObject(account));


                ViewData["username"] = HttpContext.Session.GetString("username");
                ViewBag.Notification = "Register succesfuly, go to login";
                return View();
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}
