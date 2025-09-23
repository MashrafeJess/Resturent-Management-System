using System.Security.Claims;
using Business;
using Business.Services;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Accounts
{
    [AllowAnonymous]
    public class RegistrationModel : PageModel
    {
        [BindProperty]
        public UserInfo user { get; set; } = new UserInfo();
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            user.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Result result = new UserService().Registration(user);
            if (result.Success)
            {
                return RedirectToPage("/Accounts/Login");
            }   
            else return Page();
        }
    }
}