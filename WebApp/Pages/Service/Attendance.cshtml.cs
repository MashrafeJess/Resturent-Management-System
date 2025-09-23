using Business;
using Database;
using Database.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace WebApp.Pages.Service
{
    [Authorize(Roles = "1")]
    public class AttendanceModel : PageModel
    {
        [BindProperty]
        public List<User_Attendance> users { get; set; } = new List<User_Attendance>();
        public Attendance attendance { get; set; } = new Attendance();
        public void OnGet()
        {
            Result result = new AttendanceService().JoinListByRole();
            users = result.Data as List<User_Attendance>;
        }
        public IActionResult OnPostCheckIn(string UserId)
        {
            Result result = new AttendanceService().CheckIn(UserId);
            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToPage();
        }
        public IActionResult OnPostCheckOut(string UserId)
        {
            Result result = new AttendanceService().CheckOut(UserId);
            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToPage();
        }
    }
}
