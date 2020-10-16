using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InspecWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        // private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost("Register")]
        // [HttpGet("{id}")]
        // [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDetails userDetails)
        {
            if (!ModelState.IsValid || userDetails == null)
            {
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            }

            var identityUser = new ApplicationUser() { UserName = userDetails.UserName, Email = userDetails.Email };
            var result = await userManager.CreateAsync(identityUser, userDetails.Password);
            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (IdentityError error in result.Errors)
                {
                    dictionary.AddModelError(error.Code, error.Description);
                }

                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
            }

            return Ok(new { Message = "User Reigstration Successful" });
        }

        // [HttpPost]
        [HttpPost("Login")]
        // [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
        {
            if (!ModelState.IsValid || credentials == null)
            {
                 return Ok(new { Message = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง", status = false });
            }

            var identityUser = await userManager.FindByNameAsync(credentials.Username);
            if (identityUser == null)
            {
                 return Ok(new { Message = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง", status = false });
            }

            var result = userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);
            if (result == PasswordVerificationResult.Failed)
            {

                 return Ok(new { Message = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง", status = false });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, identityUser.Email),
                new Claim(ClaimTypes.Name, identityUser.UserName)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await signInManager.PasswordSignInAsync(credentials.Username, credentials.Password, true, lockoutOnFailure: true);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return Ok(new { Message = "You are logged in", status = true });
            // return LocalRedirect(credentials.ReturnUrl != null ? credentials.ReturnUrl : "/");
        }

        // [HttpPost]
        [HttpPost("Logout")]
        // [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await signInManager.SignOutAsync();
            return Ok(new { Message = "You are logged out" });
        }

    }
}