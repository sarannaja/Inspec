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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _context;

        // private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthController (SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
            _signInManager = signInManager;
            this.userManager = userManager;
            _context = context;

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

            if (!ModelState.IsValid || credentials == null) {
                return Ok (new { Message = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง", status = false });
            } else {

                var result1 = await _signInManager.PasswordSignInAsync (credentials.Username, credentials.Password, credentials.Jodjumchan = false, lockoutOnFailure : true);
                var identityUser = await userManager.FindByNameAsync (credentials.Username);
                if (result1.IsLockedOut) {
                    var test = userManager.Options.Lockout.DefaultLockoutTimeSpan.TotalMinutes;

                    return Ok (new { Message = "คุณทำการเข้าระบบผิดพลาดเกิน 5 ครั้ง กรุณาล็อคอินใหม่ในอีก 5 นาที", status = false });
                } else if (result1.Succeeded) {
                    var result = userManager.PasswordHasher.VerifyHashedPassword (identityUser, identityUser.PasswordHash, credentials.Password);
                    var test2 = userManager.Options.Lockout.MaxFailedAccessAttempts;
                    var claims = new List<Claim> {
                        new Claim (ClaimTypes.Email, identityUser.Email),
                        new Claim (ClaimTypes.Name, identityUser.UserName)
                    };

                    var claimsIdentity = new ClaimsIdentity (
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await _signInManager.PasswordSignInAsync (credentials.Username, credentials.Password, credentials.Jodjumchan, lockoutOnFailure : true);
                    // var UserId = userManager.GetUserIdAsync (identityUser);
                    var user = _context.Users.Where (us => us.UserName == credentials.Username).First ();

                    // userManager.get (claimsIdentity)
                    // await HttpContext.SignInAsync (
                    //     CookieAuthenticationDefaults.AuthenticationScheme,
                    //     new ClaimsPrincipal (claimsIdentity));

                    if (user.Active == 1) {
                        return Ok (new { Message = "You are logged in", status = true });
                    } else {
                        return Ok (new { Message = "คุณไม่มีสิทธิ์เข้าใช้งานระบบ กรุณาติดต่อผู้ดูแลระบบ", status = false });
                    }

                } else {
                    if (identityUser != null) {

                        var UserId = userManager.GetUserIdAsync (identityUser);
                        var AccessFailedCount = _context.Users.Where (us => us.UserName == credentials.Username).First ().AccessFailedCount;
                        return Ok (new { Message = $"คุณทำการเข้าสู่ระบบผิดพลาดแล้ว {AccessFailedCount} ครั้ง ถ้าเข้าสู่ระบบผิดพลาดเกิน 5 ครั้งคุณไม่สามารถเข้าสู่ระบบได้เป็นเวลา 5 นาที", status = false, identityUser });
                    } else {
                        return Ok (new { Message = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง", status = false });
                    }
                    // if (result == PasswordVerificationResult.Failed) {
                    //     // return Ok (new { Message = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง", status = false });
                    // }
                }
            }

            // return LocalRedirect (credentials.ReturnUrl != null ? credentials.ReturnUrl : "/inspectionplanevent/all");
        }

        [HttpPost ("Login2")]
        public async Task<IActionResult> Login2 ([FromBody] LoginCredentials credentials) {
            if (ModelState.IsValid) {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync (credentials.Username, credentials.Password, credentials.Jodjumchan, lockoutOnFailure : true);
                // var result = await __signInManager.PasswordSignInAsync(userModel.Email, userModel.Password, userModel.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded) {
                    // _logger.LogInformation ("User logged in.");
                    return Ok (new { ReturnUrl = credentials.ReturnUrl, status = false });
                }
                if (result.RequiresTwoFactor) {
                    return RedirectToPage ("./LoginWith2fa", new { ReturnUrl = credentials.ReturnUrl, RememberMe = true });
                }
                if (result.IsLockedOut) {
                    //var forgotPassLink = Url.Action(nameof(ForgotPassword), "Account", new { }, Request.Scheme);
                    //var content = string.Format("Your account is locked out, to reset your password, please click this link: {0}", forgotPassLink);

                    //var message = new Message(new string[] { userModel.Email }, „Locked out account information“, content, null);
                    //await _emailSender.SendEmailAsync(message);

                    ModelState.AddModelError ("", "คุณทำการเข้าระบบผิดพลาดเกิน 5 ครั้ง กรุณาล็อคอินใหม่ในอีก 2 นาที");
                    return Ok (new { Message = "คุณทำการเข้าระบบผิดพลาดเกิน 5 ครั้ง กรุณาล็อคอินใหม่ในอีก 2 นาที", status = false });

                    //_logger.LogWarning("User account locked out.");
                    //return RedirectToOk("./Lockout");
                } else {
                    ModelState.AddModelError (string.Empty, "Invalid login attempt.");
                    return Ok (new { Message = "Invalid login attempt.", status = false, credentials, result });
                }

            }
            return Ok (new { Message = "Out Of control.", status = false });
        }

        // [HttpPost]
        [HttpPost ("Logout")]
        // [Route("Logout")]
        public async Task<IActionResult> Logout () {
            await HttpContext.SignOutAsync (CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync ();
            return Ok (new { Message = "You are logged out" });
        }

    }
}