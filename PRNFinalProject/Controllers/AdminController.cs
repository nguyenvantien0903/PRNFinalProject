using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRNFinalProject.Data;
using PRNFinalProject.Models;

namespace PRNFinalProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly CenimaDBContext _context;

        public AdminController(CenimaDBContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> ListUser()
        {
            return View(await _context.Persons.ToListAsync());
        }
    }
}
