using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using InspecWeb.Data;
using InspecWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Logging;

namespace InspecWeb.Areas.Identity.Pages.Account
{
    // [AllowAnonymous]

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
            this._userManager = userManager;
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
                ModelState.AddModelError("", ErrorMessage);
            }
            // var claims = ClaimsPrincipal.Current.Identity.IsAuthenticated;
            // if (claims)
            // {

            // }
            // var test = claims.Claims.ToList().FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.OrdinalIgnoreCase));
            returnUrl = returnUrl ?? Url.Content("~/");


            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _signInManager.SignOutAsync();
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ReturnUrl = returnUrl;
            _logger.LogWarning(_signInManager.GetExternalLoginInfoAsync().IsCompleted.ToString());
            LocalRedirect("/");
        }

public async Task<IActionResult> OnPostAsync(
    string returnUrl = null)
{
    Console.WriteLine("=================================");
    Console.WriteLine("===== OnPostAsync START =====");
    Console.WriteLine("=================================");

    returnUrl =
        returnUrl ??
        Url.Content("~/main");


    // ==========================================
    // Debug Input
    // ==========================================

    Console.WriteLine(
        "Username = " + Input?.Username
    );

    Console.WriteLine(
        "Password Is Empty = " +
        string.IsNullOrEmpty(Input?.Password)
    );


    // ==========================================
    // ModelState
    // ==========================================

    if (!ModelState.IsValid)
    {
        Console.WriteLine(
            "===== MODEL STATE INVALID ====="
        );

        foreach (var item in ModelState)
        {
            foreach (var error in item.Value.Errors)
            {
                Console.WriteLine(
                    "FIELD = " +
                    item.Key +
                    " ERROR = " +
                    error.ErrorMessage
                );
            }
        }

        return Page();
    }


    // ==========================================
    // Find User
    // ==========================================

    var identityUser =
        await _userManager.FindByNameAsync(
            Input.Username
        );

    if (identityUser == null)
    {
        Console.WriteLine(
            "USER NOT FOUND"
        );

        ModelState.AddModelError(
            "",
            "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง"
        );

        return Page();
    }


    Console.WriteLine(
        "USER FOUND = " +
        identityUser.UserName
    );


    // ==========================================
    // Check Password
    // ==========================================

    var result =
        await _signInManager.CheckPasswordSignInAsync(
            identityUser,
            Input.Password,
            lockoutOnFailure: true
        );


    Console.WriteLine(
        "Password Succeeded = " +
        result.Succeeded
    );

    Console.WriteLine(
        "Password IsLockedOut = " +
        result.IsLockedOut
    );

    Console.WriteLine(
        "Password IsNotAllowed = " +
        result.IsNotAllowed
    );

    Console.WriteLine(
        "Password RequiresTwoFactor = " +
        result.RequiresTwoFactor
    );


    // ==========================================
    // Password ถูกต้อง
    // ==========================================

    if (result.Succeeded)
    {

        Console.WriteLine(
            "===== PASSWORD SUCCESS ====="
        );


        // ==========================================
        // Find Application User
        // ==========================================

        var user =
            _context.Users
                .FirstOrDefault(
                    us =>
                        us.UserName ==
                        Input.Username
                );


        if (user == null)
        {
            Console.WriteLine(
                "APPLICATION USER NOT FOUND"
            );

            ModelState.AddModelError(
                "",
                "ไม่พบข้อมูลผู้ใช้งาน"
            );

            return Page();
        }


        Console.WriteLine(
            "APPLICATION USER FOUND"
        );

        Console.WriteLine(
            "USERNAME = " +
            user.UserName
        );

        Console.WriteLine(
            "ACTIVE = " +
            user.Active
        );


        // ==========================================
        // Check Active
        // ==========================================

        if (user.Active == 1)
        {

            Console.WriteLine(
                "===== SHOW PIN MODAL ====="
            );


            TempData["ShowPinModal"] =
                "true";

            TempData["ReturnUrl"] =
                returnUrl;

            TempData["PinUsername"] =
                user.UserName;


            return Page();
        }


        // ==========================================
        // Not Active
        // ==========================================

        Console.WriteLine(
            "USER NOT ACTIVE"
        );

        ModelState.AddModelError(
            "",
            "คุณไม่มีสิทธิ์เข้าใช้งานระบบ กรุณาติดต่อผู้ดูแลระบบ"
        );

        return Page();
    }


    // ==========================================
    // Locked Out
    // ==========================================

    if (result.IsLockedOut)
    {

        Console.WriteLine(
            "USER LOCKED OUT"
        );

        ModelState.AddModelError(
            "",
            "คุณทำการเข้าระบบผิดพลาดเกิน 5 ครั้ง กรุณาล็อคอินใหม่ในอีก 5 นาที"
        );

        return Page();
    }


    // ==========================================
    // Requires Two Factor
    // ==========================================

    if (result.RequiresTwoFactor)
    {

        Console.WriteLine(
            "REQUIRES TWO FACTOR"
        );

        return RedirectToPage(
            "./LoginWith2fa",
            new
            {
                ReturnUrl = returnUrl,
                RememberMe = Input.RememberMe
            }
        );
    }


    // ==========================================
    // Not Allowed
    // ==========================================

    if (result.IsNotAllowed)
    {

        Console.WriteLine(
            "USER NOT ALLOWED"
        );

        ModelState.AddModelError(
            "",
            "บัญชีผู้ใช้งานนี้ไม่สามารถเข้าสู่ระบบได้"
        );

        return Page();
    }


    // ==========================================
    // Password ไม่ถูกต้อง
    // ==========================================

    Console.WriteLine(
        "===== PASSWORD INCORRECT ====="
    );

    ModelState.AddModelError(
        "",
        $"คุณทำการเข้าสู่ระบบผิดพลาดแล้ว " +
        $"{identityUser.AccessFailedCount} ครั้ง " +
        "ถ้าเข้าสู่ระบบผิดพลาดเกิน 5 ครั้ง " +
        "คุณไม่สามารถเข้าสู่ระบบได้เป็นเวลา 5 นาที"
    );

    return Page();
}

public async Task<IActionResult> OnPostPinAsync(string pin)
{
    var username = TempData.Peek("PinUsername")?.ToString();

    var returnUrl = TempData.Peek("ReturnUrl")?.ToString()
                    ?? Url.Content("~/main");

    if (string.IsNullOrEmpty(username))
    {
        return new JsonResult(new
        {
            success = false,
            message = "Session หมดอายุ กรุณาเข้าสู่ระบบใหม่"
        });
    }

    var user = _context.Users
        .FirstOrDefault(us => us.UserName == username);

    if (user == null)
    {
        return new JsonResult(new
        {
            success = false,
            message = "ไม่พบข้อมูลผู้ใช้งาน"
        });
    }

    if (user.Pin != pin)
    {
        return new JsonResult(new
        {
            success = false,
            message = "PIN ไม่ถูกต้อง"
        });
    }

    var identityUser =
        await _userManager.FindByNameAsync(username);

    if (identityUser == null)
    {
        return new JsonResult(new
        {
            success = false,
            message = "ไม่พบข้อมูล Account"
        });
    }

    await _signInManager.SignInAsync(
        identityUser,
        isPersistent: false);

    return new JsonResult(new
    {
        success = true,
        redirectUrl = returnUrl
    });
}

    }
}