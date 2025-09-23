using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Business;
namespace WebApp.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<string> roles { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {

            _logger = logger;
        }

        public void OnGet()
        {
            roles = new List<string>()
            { "1","2","3" };
            //Result result = new Top10Service().ListTop10Events();
            //top = result.Data as List<Top10Images> ?? new List<Top10Images>();
        }
    }
}