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
    public class CartListModel : PageModel
    {
        [BindProperty]
        public List<JoinCart> carts { get; set; } = new List<JoinCart>();

        public void OnGet()
        {
            carts = new CartService().List().Data as List<JoinCart>;
        }
        public IActionResult OnPostUpdateQty(int id, int qty)
        {
            Result result = new CartService().UpdateQty(id, qty);
            if(result.Success)
            {
                TempData["Success"] = result.Message;
            }
            else
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToPage();
        }
        public IActionResult OnPostDelete(int CartId)
        {
            Result result = new CartService().Delete(CartId);
            if (result.Success)
            {
                TempData["Success"] = result.Message;
            }
            else
            {
                TempData["Error"] = result.Message;
            }
            return RedirectToPage();
        }
    }
}