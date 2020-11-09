using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InspecWeb.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _context;
        // private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthController (SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
            this.signInManager = signInManager;
            _context = context;
            this.userManager = userManager;
        }

        [HttpPost ("Register")]
        // [HttpGet("{id}")]
        // [Route("Register")]
        public async Task<IActionResult> Register ([FromBody] UserDetails userDetails) {
            if (!ModelState.IsValid || userDetails == null) {
                return new BadRequestObjectResult (new { Message = "User Registration Failed" });
            }

            var identityUser = new ApplicationUser () { UserName = userDetails.UserName, Email = userDetails.Email };
            var result = await userManager.CreateAsync (identityUser, userDetails.Password);
            if (!result.Succeeded) {
                var dictionary = new ModelStateDictionary ();
                foreach (IdentityError error in result.Errors) {
                    dictionary.AddModelError (error.Code, error.Description);
                }

                return new BadRequestObjectResult (new { Message = "User Registration Failed", Errors = dictionary });
            }

            return Ok (new { Message = "User Reigstration Successful" });
        }

        // [HttpPost]
        [HttpPost ("Login")]
        // [Route("Login")]
        public async Task<IActionResult> Login ([FromBody] LoginCredentials credentials) {

            // var test2 = userManager.Options.Lockout.MaxFailedAccessAttempts;
            // return Ok(UserId);
            if (!ModelState.IsValid || credentials == null) {
                // var test = userManager.Options
                // return Ok (new { Message = $"คุณทำการเข้าสู่ระบบผิดพลาดแล้ว {AccessFailedCount} ครั้ง ถ้าเข้าสู่ระบบผิดพลาดเกิน 5 ครั้งคุณไม่สามารถเข้าสู่ระบบได้เป็นเวลา 5 นาที", status = false });
                return Ok (new { Message = "ชื่อผู้ใช้ หรือ รหัสผ่านไม่ถูกต้อง", status = false });
            } else {
                var result1 = await signInManager.PasswordSignInAsync (credentials.Username, credentials.Password, credentials.Jodjumchan = false, lockoutOnFailure : true);

                var identityUser = await userManager.FindByNameAsync (credentials.Username);
                var result = userManager.PasswordHasher.VerifyHashedPassword (identityUser, identityUser.PasswordHash, credentials.Password);
                if (result1.IsLockedOut) {
                    // var test = userManager.Options.Lockout.DefaultLockoutTimeSpan.TotalMinutes;

                    return Ok (new { Message = "คุณทำการเข้าระบบผิดพลาดเกิน 5 ครั้ง กรุณาล็อคอินใหม่ในอีก 5 นาที", status = false });
                } else if (result1.Succeeded) {
                    var claims = new List<Claim> {
                        new Claim (ClaimTypes.Email, identityUser.Email),
                        new Claim (ClaimTypes.Name, identityUser.UserName)
                    };

                    var claimsIdentity = new ClaimsIdentity (
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    // var result1 = await signInManager.PasswordSignInAsync(credentials.Username, credentials.Password, false, lockoutOnFailure: true);

                    await HttpContext.SignInAsync (
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal (claimsIdentity));
                    // return LocalRedirect (credentials.ReturnUrl != null ? credentials.ReturnUrl : "/main");
                    return Ok (new { Message = "You are logged in", status = true });
                } else {
                    var UserId = await userManager.GetUserIdAsync (identityUser);
                    var AccessFailedCount = _context.Users.Find (UserId).AccessFailedCount;

                    if (result == PasswordVerificationResult.Failed) {
                        // var UserId = await userManager.GetUserIdAsync (identityUser);
                        // var AccessFailedCount = _context.Users.Find (UserId).AccessFailedCount;
                        return Ok (new { Message = $"คุณทำการเข้าสู่ระบบผิดพลาดแล้ว {AccessFailedCount} ครั้ง ถ้าเข้าสู่ระบบผิดพลาดเกิน 5 ครั้งคุณไม่สามารถเข้าสู่ระบบได้เป็นเวลา 5 นาที", status = false });
                    }
                }
            }

            // return LocalRedirect (credentials.ReturnUrl != null ? credentials.ReturnUrl : "/main");
            return Ok (new { Message = "You are logged in", status = true });
        }

        // [HttpPost]
        [HttpPost ("Logout")]
        // [Route("Logout")]
        public async Task<IActionResult> Logout () {
            await HttpContext.SignOutAsync (CookieAuthenticationDefaults.AuthenticationScheme);
            await signInManager.SignOutAsync ();
            return Ok (new { Message = "You are logged out" });
        }

    }
}