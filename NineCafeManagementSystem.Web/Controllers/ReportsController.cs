namespace NineCafeManagementSystem.Web.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class ReportsController(IDashboardService _dashboardService) : Controller
    {
        public async Task<IActionResult> Index(int? year=null, int? month=null)
        {
            var now = DateTime.Now;
            var reportYear = year ?? now.Year;
            var reportMonth = month ?? now.Month;

            var report = await _dashboardService.GetMonthlyReportAsync(reportYear, reportMonth);

            // prepare year/month lists for dropdowns
            ViewBag.Years = Enumerable.Range(now.Year - 2, 3).ToList(); // last 2 years + current
            ViewBag.Months = Enumerable.Range(1, 12);

            return View(report);
        }
    }
}
