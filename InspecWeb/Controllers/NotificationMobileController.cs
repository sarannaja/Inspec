using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    public class NotificationMobileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotificationMobileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult PostToken([FromBody] UserTokenMobile model)
        {
            var data = _context.UserTokenMobiles
            .Where(w => w.Token == model.Token)
            .ToArray();
            if (data.Count() == 0 && model.Session != null)
            {

                _context.UserTokenMobiles.Add(model);
                _context.SaveChanges();
                var data2 = _context.UserTokenMobiles
                .Where(w => w.Token == model.Token)
                .ToArray();
                // return Ok(data);
                return Ok(new { data2, status = "add" });
            }
            else if (data.Count() != 0 && model.Session == null)
            // else
            {
                _context.UserTokenMobiles.RemoveRange(data);
                _context.SaveChanges();
                var data2 = _context.UserTokenMobiles
                .Where(w => w.Token == model.Token)
                .ToArray();
                return Ok(new { data2, status = "delete" });
            }
            else
            {
                // var date = DateTime.Now;
                foreach (var item in data)
                {
                    var UserEditUserid = _context.UserTokenMobiles.Find(item.Id);
                    // new UserTokenMobile
                    // {
                    //     UserID = model.UserID
                    // };
                    UserEditUserid.UserID = model.UserID;
                    _context.Entry(UserEditUserid).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                }
                var data2 = _context.UserTokenMobiles
                                .Where(w => w.Token == model.Token)
                                .ToArray();
                return Ok(new { data2, status = "update UserID" });
                // return Ok(data);
            }



        }



    }
}
