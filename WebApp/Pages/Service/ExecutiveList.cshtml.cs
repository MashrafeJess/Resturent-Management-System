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
    public class ExecutiveListModel : PageModel
    {
        [BindProperty]
        public List<User_Role>users { get; set; } = new List<User_Role>();  

        public void OnGet()
        {
            users = new UserService().List().Data as List<User_Role>;
        }
    }
}