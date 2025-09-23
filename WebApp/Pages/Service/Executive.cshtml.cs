using System.Security.Claims;
using Business;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Database.ViewModel;
namespace WebApp.Pages.Service
{
    [Authorize(Roles = "1")]
    public class ExecutiveModel : PageModel
    {
        [BindProperty]
        public List<User_Role>? users { get; set; } 

        public string LoggedInUser { get; set; }

        public void OnGet(string? Id = null)
        {
            LoggedInUser = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (LoggedInUser != null)
            {
                Result result = new UserService().Single(LoggedInUser);
                UserInfo userData = result.Data as UserInfo;
                user.Name = userData.Name;
                user.Email = userData.Email;
                user.Contact = userData.Contact;
                user.Role = userData.Role;
                user.PasswordHash = userData.PasswordHash; // Assuming you want to allow password change
            }
        }

        public IActionResult OnPost()
        {
            user.UpdatedBy = LoggedInUser;
            Result result = new UserService().Update(user);
            if (result.Success)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }
        }
    }
}