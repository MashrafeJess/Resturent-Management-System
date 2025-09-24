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
    [Authorize(Roles = "1,2,3")]
    public class MenuModel : PageModel
    {
        [BindProperty]
        public List<Food> foods { get; set; } = new List<Food>();

        public void OnGet()
        {
            foods = new FoodService().List().Data as List<Food>;
        }
        public IActionResult OnPost(int FoodId, int Qty ,int Price)
        {
            Cart cart = new Cart
            {
                FoodId = FoodId,
                Qty = Qty,
                Price = Qty * Price,
                CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            };
            Result result = new CartService().Add(cart);
            if (result.Success)
            {
                TempData["Success"] = result.Message;
                return RedirectToPage("/Index");
            }
            else
            {
                TempData["Error"] = result.Message;
                return Page();
            }  
        }
    }
}