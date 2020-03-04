using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InspecWeb.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private static UserManager<ApplicationUser> _userManager;
        private static ApplicationDbContext _context;

        public UserController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        [Route("[controller]/[action]")]
        public async Task<string> Create()
        {
            string result = "Null";
            
            if (_context.Users.Count() == 0)
            {   
                var user = new ApplicationUser { UserName = "admin@inspec.go.th", Email = "admin@inpsec.go.th" };
                var success = await _userManager.CreateAsync(user, "Admin@12345678").ConfigureAwait(false);

                if (success.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(user, "ScreateAdministrator").ConfigureAwait(false);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
                    await _userManager.ConfirmEmailAsync(user, code).ConfigureAwait(false);

                    result = "Success";
                }
                else
                {
                    result = "Fail";
                }
            }
            else
            {
                result = "Fail";
            }

            return result;
        }
    }
}