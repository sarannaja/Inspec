using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using InspecWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using InspecWeb.Data;

namespace InspecWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationDbContext _context;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
             ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)

        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            //[EmailAddress]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "จดจำฉัน")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/main");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    var user = _context.Users.Where(us => us.UserName == Input.Username).First();
                    if (user.Active == 1)
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "คุณไม่มีสิทธิ์เข้าใช้งานระบบ กรุณาติดต่อผู้ดูแลระบบ");
                        await _signInManager.SignOutAsync();
                        return Page();

                    }

                    // return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "คุณทำการเข้าระบบผิดพลาดเกิน 5 ครั้ง กรุณาล็อคอินใหม่ในอีก 2 นาที");
                    await _signInManager.SignOutAsync();
                    return Page();
                }

                else

                {

                    var identityUser = await _userManager.FindByEmailAsync(Input.Username);

                    // var userr = 
                    if (identityUser != null)
                    {
                        ModelState.AddModelError(string.Empty, $"คุณทำการเข้าสู่ระบบผิดพลาดแล้ว {identityUser.AccessFailedCount.ToString()} ครั้ง ถ้าเข้าสู่ระบบผิดพลาดเกิน 5 ครั้งคุณไม่สามารถเข้าสู่ระบบได้เป็นเวลา 5 นาที");
                        return Page();
                    }
                    else
                    {

                        ModelState.AddModelError(string.Empty, "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง" + identityUser.AccessFailedCount.ToString());
                        return Page();
                    }
                    // _logger.LogDebug(user.Id);
                    // if (user != null)
                    // {
                    //     ModelState.AddModelError(string.Empty, "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
                    //     return Page();
                    // }
                    // else
                    // {
                    //     ModelState.AddModelError(string.Empty, $"คุณทำการเข้าสู่ระบบผิดพลาดแล้ว {user.AccessFailedCount} ครั้ง ถ้าเข้าสู่ระบบผิดพลาดเกิน 5 ครั้งคุณไม่สามารถเข้าสู่ระบบได้เป็นเวลา 5 นาที");
                    //     return Page();
                    //     // return Ok(new { Message = , status = false, identityUser });
                    // }

                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
