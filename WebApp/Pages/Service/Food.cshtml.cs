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
    public class FoodModel : PageModel
    {
        [BindProperty]
        public Food food { get; set; } = new Food();

        public void OnGet(int? Id = null)
        {
            int idValue = Id ?? 0; // Use 0 if null

            if (idValue != 0)
            {
                food = new FoodService().Single(idValue).Data as Food;
            }
        }

        public IActionResult OnPost()
        {
            Result result;
            if (food.FoodId == 0)
            {
                food.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                result = new FoodService().Add(food);
                if (result.Success)
                {
                    TempData["Success"] = result.Message;
                    return RedirectToPage("/Service/FoodList");
                }
                else
                {
                    TempData["Error"] = result.Message;
                    return Page();
                }
            }
            else
            {
                food.UpdatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; ;
                food.UpdatedDate = DateTime.Now;
                result = new FoodService().Update(food);
                if (result.Success)
                {
                    TempData["Success"] = result.Message;
                    return RedirectToPage("/Service/FoodList");
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