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
    [Authorize(Roles = "1,2")]
    public class FoodListModel : PageModel
    {
        [BindProperty]
        public List<Food >foods { get; set; } = new List<Food>();

        public void OnGet()
        {
            foods = new FoodService().List().Data as List<Food>;
        }
    }
}