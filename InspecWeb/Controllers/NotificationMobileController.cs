using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspecWeb.Controllers {
    [Route ("api/[controller]")]
    public class NotificationMobileController : Microsoft.AspNetCore.Mvc.Controller {

        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public NotificationMobileController (ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get () {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync (User);
            //    var email = user.Email;
            return Ok (user);
            // User?.Claims
        }

        [HttpPost ("send")]
        public async Task<IActionResult> GetUser () {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync (User);
            //    var email = user.Email;
            return Ok (user);
            // User?.Claims
        }

        [HttpPost]
        public IActionResult PostToken ([FromBody] UserTokenMobile model) {

            var data = _context.UserTokenMobiles
                .Where (w => w.Token == model.Token)
                .ToArray ();
            if (data.Count () == 0 && model.Session != null) {

                _context.UserTokenMobiles.Add (model);
                _context.SaveChanges ();
                var data2 = _context.UserTokenMobiles
                    .Where (w => w.Token == model.Token)
                    .ToArray ();
                // return Ok(data);
                return Ok (new { data2, status = "add" });
            } else if (data.Count () != 0 && model.Session == null)
            // else
            {
                _context.UserTokenMobiles.RemoveRange (data);
                _context.SaveChanges ();
                var data2 = _context.UserTokenMobiles
                    .Where (w => w.Token == model.Token)
                    .ToArray ();
                return Ok (new { data2, status = "delete" });
            } else {
                // var date = DateTime.Now;
                foreach (var item in data) {
                    var UserEditUserid = _context.UserTokenMobiles.Find (item.Id);
                    // new UserTokenMobile
                    // {
                    //     UserID = model.UserID
                    // };
                    UserEditUserid.UserID = model.UserID;
                    _context.Entry (UserEditUserid).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges ();
                }
                var data2 = _context.UserTokenMobiles
                    .Where (w => w.Token == model.Token)
                    .ToArray ();
                return Ok (new { data2, status = "update UserID" });
                // return Ok(data);
            }

        }

        [HttpPut]
        public IActionResult UpdateUserToken ([FromBody] UserTokenMobile model) {
            var data = _context.UserTokenMobiles
                .Where (w => w.Session == model.Session)
                .ToArray ();
            if (data.Count () == 0) {
                // return Ok(data);
                return Ok (new { data, status = "not" });

            } else {
                // var date = DateTime.Now;
                foreach (var item in data) {
                    var UserEditUserid = _context.UserTokenMobiles.Find (item.Id);
                    // new UserTokenMobile
                    // {
                    //     UserID = model.UserID
                    // };
                    UserEditUserid.UserID = model.UserID;
                    _context.Entry (UserEditUserid).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges ();
                }
                var data2 = _context.UserTokenMobiles
                    .Where (w => w.Session == model.Session)
                    .ToArray ();
                return Ok (new { data2, status = "update UserID" });
                // return Ok(data);
            }

        }

    }
}