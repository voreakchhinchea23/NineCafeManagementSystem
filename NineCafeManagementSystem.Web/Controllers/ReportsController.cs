

namespace NineCafeManagementSystem.Web.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class ReportsController(IReportService _reportService) : Controller
    {
        public async Task<IActionResult> Index(int? year=null, int? month=null)
        {
            var now = DateTime.Now;
            var reportYear = year ?? now.Year;
            var reportMonth = month ?? now.Month;

            var report = await _reportService.GetMonthlyReportAsync(reportYear, reportMonth);

            // prepare year/month lists for dropdowns
            report.AvailableYears = Enumerable.Range(now.Year - 2, 3).Reverse().ToList(); // last 2 years + current
            report.AvailableMonths = Enumerable.Range(1, 12).ToList();

            return View(report);
        }
        public async Task<IActionResult> ExportMonthly(int year, int month)
        {
            var fileBytes = await _reportService.GenerateMonthlyReportExcelAsync(year, month);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"NineCafe_Report_{year}_{month:00}.xlsx");
        }
    }
}
