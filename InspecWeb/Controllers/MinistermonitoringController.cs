﻿using System;
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
    [Route("api/[controller]")]
    public class MinistermonitoringController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MinistermonitoringController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Models.Ministermonitoring> Get()
        {
            var centralpolicydata = from P in _context.Ministermonitorings
                                     .Include(m => m.MinistermonitoringRegions)
                                     .ThenInclude(m => m.Region)
                                    select P;

            return centralpolicydata;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public Cabine Post(string name, string position, string image)
        {
            var date = DateTime.Now;

            var cabinedata = new Cabine
            {
                Name = name,
                Position = position,
                Image = image,
                CreatedAt = date
            };

            _context.Cabines.Add(cabinedata);
            _context.SaveChanges();

            return cabinedata;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(long id, string name, string position, string image)
        {
            var cabine = _context.Cabines.Find(id);
            cabine.Name = name;
            cabine.Position = position;
            cabine.Image = image;
            _context.Entry(cabine).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var cabinedata = _context.Cabines.Find(id);

            _context.Cabines.Remove(cabinedata);
            _context.SaveChanges();
        }
    }

    public class Ministermonitoring
    {
    }
}