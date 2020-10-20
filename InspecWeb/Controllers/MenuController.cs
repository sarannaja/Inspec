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

        [HttpPut("api/[controller]/{id}")]
        public void Put([FromForm] MenuRequest request, long id)
        {

            var menu = _context.Menu.Find(id);
            menu.M1 = request.m1;
            menu.M2 = request.m2;
            menu.M3 = request.m3;
            menu.M4 = request.m4;
            menu.M5 = request.m5;
            menu.M6 = request.m6;
            menu.M7 = request.m7;
            menu.M8 = request.m8;
            menu.M9 = request.m9;
            menu.M10 = request.m10;
            menu.M11 = request.m11;
            menu.M12 = request.m12;
            menu.M13 = request.m13;
            menu.M14 = request.m14;
            menu.M15 = request.m15;
            menu.M16 = request.m16;
            menu.M17 = request.m17;
            menu.M18 = request.m18;
            menu.M19 = request.m19;
            menu.M20 = request.m20;
            menu.M21 = request.m21;
            menu.M22 = request.m22;
            menu.M23 = request.m23;
            menu.M24 = request.m24;
            menu.M25 = request.m25;
            menu.M26 = request.m26;
            menu.M27 = request.m27;
            menu.M28 = request.m28;
            menu.M29 = request.m29;
            menu.M30 = request.m30;
            menu.M31 = request.m31;
            menu.M32 = request.m32;
            menu.M33 = request.m33;
            menu.M34 = request.m34;


            _context.Entry(menu).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

    }
}

public class MenuRequest
{
    public long Id { get; set; }
    public long m1 { get; set; }
    public long m2 { get; set; }
    public long m3 { get; set; }
    public long m4 { get; set; }
    public long m5 { get; set; }
    public long m6 { get; set; }
    public long m7 { get; set; }
    public long m8 { get; set; }
    public long m9 { get; set; }
    public long m10 { get; set; }
    public long m11 { get; set; }
    public long m12 { get; set; }
    public long m13 { get; set; }
    public long m14 { get; set; }
    public long m15 { get; set; }
    public long m16 { get; set; }
    public long m17 { get; set; }
    public long m18 { get; set; }
    public long m19 { get; set; }
    public long m20 { get; set; }
    public long m21 { get; set; }
    public long m22 { get; set; }
    public long m23 { get; set; }
    public long m24 { get; set; }
    public long m25 { get; set; }
    public long m26 { get; set; }
    public long m27 { get; set; }
    public long m28 { get; set; }
    public long m29 { get; set; }
    public long m30 { get; set; }
    public long m31 { get; set; }
    public long m32 { get; set; }
    public long m33 { get; set; }
    public long m34 { get; set; }
}