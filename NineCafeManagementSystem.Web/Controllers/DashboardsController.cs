namespace NineCafeManagementSystem.Web.Controllers
{
    public class DashboardsController(IDashboardService _dashboardService) : Controller
    {
        public async Task<IActionResult> Index(DateOnly? date = null)
        {
            var selectedDate = date ?? DateOnly.FromDateTime(DateTime.Today);

            var priceTier = await _dashboardService.LoadPriceTierAsync();
            var todaySales = await _dashboardService.GetSaleByDateAsync(selectedDate);

            var model = new DashboardVM 
            { 
                PriceTiers = priceTier,
                SelectedDate = selectedDate,
                TodaySales = todaySales,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecordSale(int priceRiel)
        {
            await _dashboardService.RecordSaleAsync(priceRiel);
            return RedirectToAction(nameof(Index));
        }
    }
}
