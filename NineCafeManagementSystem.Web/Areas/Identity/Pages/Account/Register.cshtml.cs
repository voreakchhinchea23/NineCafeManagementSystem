

namespace NineCafeManagementSystem.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        //Registration is disabled — no dependencies needed
        public IActionResult OnGet()
        {
            return RedirectToPage("/Account/Login");
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("/Account/Login");
        }
    }
}