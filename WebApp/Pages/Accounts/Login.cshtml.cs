using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Business;
using Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Accounts
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        [BindProperty]
        public UserInfo user { get; set; } = new UserInfo();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Result result = new UserService().Login(user);
            if (result.Success)
            {
                UserInfo user = result.Data as UserInfo;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Role,user.Role.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToPage("/Index");
            }
            else
            {
                TempData["Error"] = result.Message;
                return Page();
            }
        }
        public async Task<IActionResult> OnGetLogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}