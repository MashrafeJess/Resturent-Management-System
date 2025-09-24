using System.Security.Claims;
using Business;
using Business.Services;
using Database;
using Database.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace WebApp.Pages.Service
{
    [Authorize(Roles = "1")]
    public class ExecutiveModel : PageModel
    {
        [BindProperty]
        public UserInfo user { get; set; } = new UserInfo();
        public List<Role> role = new List<Role>();
        public bool isEdit = false;
        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }

        public void OnGet(string? Id = null)
        {
            role = new RoleService().List().Data as List<Role>;
            if (Id != null)
            {
                Result result = new UserService().JoinSingle(Id);
                User_Role userData = result.Data as User_Role;
                user.Name = userData.Name;
                user.Email = userData.Email;
                user.Contact = userData.Contact;
                user.Role = userData.Role;
                isEdit = true;
                // user.PasswordHash = userData.PasswordHash; // Assuming you want to allow password change
            }
            else
            {
                user.UserId = null;
            }
        }

        public IActionResult OnPost()
        {
            Result result;
            if(user.UserId==null)
            {
                user.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                result = new UserService().Registration(user);
                if(result.Success)
                {
                    TempData["Success"] = result.Message;
                    return RedirectToPage("/Service/ExecutiveList");
                }
                else
                {
                    TempData["Error"] = result.Message;
                    return Page();
                }
            }
            else
            {
                user.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; ;
                user.UpdatedDate = DateTime.Now;
                result = new UserService().Update(user);
                if (result.Success)
                {
                    TempData["Success"] = result.Message;
                    return RedirectToPage("/Service/ExecutiveList");
                }
                else
                {
                    TempData["Error"] = result.Message;
                    return Page();
                }
            }
        }
    }
}