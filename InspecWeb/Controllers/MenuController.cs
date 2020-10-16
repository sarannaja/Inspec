using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("api/[controller]/[action]/{id}")]
        public IActionResult getmenu(long id)
        {
            var menu = _context.Menu
                .Where(m => m.Role_id == id).FirstOrDefault();
   
            return Ok(menu);
        }

        [HttpGet("api/[controller]/[action]")]
        public IActionResult getmenulistdata()
        {
            var menu = _context.Menu;

            return Ok(menu);
        }

    }
}
