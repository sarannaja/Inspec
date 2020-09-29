using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangeUserController : ControllerBase
    {
        public static IWebHostEnvironment _environment;

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static UserManager<ApplicationUser> _userManager;
        private static ApplicationDbContext _context;

        public MangeUserController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;

        }

        [HttpPost("resetpassword")]
        public async Task<ActionResult> ResetUserPassword(string id, string Password)
        {
            //     Find User
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            if (user == null)
            {
                return Ok(new { id = id });
            }
            await _userManager.RemovePasswordAsync(user);
            //     Add a user password only if one does not already exist
            await _userManager.AddPasswordAsync(user, Password);
            return Ok(new { id = id, Password });
        }



    }
}