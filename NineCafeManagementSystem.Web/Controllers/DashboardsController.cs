using NineCafeManagementSystem.Web.Data;

namespace NineCafeManagementSystem.Web.Controllers
{
    [Authorize]
    public class DashboardsController(IDashboardService _dashboardService) : Controller
    {
        public async Task<IActionResult> Index(DateOnly? date = null)
        {
            var selectedDate = date ?? DateOnly.FromDateTime(DateTime.Today);

            var priceTier = await _dashboardService.LoadPriceTierAsync();
            var todaySales = await _dashboardService.GetSaleByDateAsync(selectedDate);
            var summary = await _dashboardService.GetDashboardSummaryAsync(selectedDate);
            var withdrawals = await _dashboardService.GetWithdrawalByDateAsync(selectedDate);

            var totalWithdrawn = withdrawals.Sum(q => q.Amount);

            var model = new DashboardVM
            {
                PriceTiers = priceTier,
                SelectedDate = selectedDate,
                TodaySales = todaySales,
                DailyIncome = summary.DailyIncome,
                TotalCupSold = summary.TotalCupSold,
                TotalWithdrawals = withdrawals,
                TotalWithdrawnToday = totalWithdrawn
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

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int priceTierId, int quantity)
        {
            await _dashboardService.EditSaleAsync(priceTierId, quantity);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RecordWithdrawal(WithdrawalCreateVM model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await _dashboardService.RecordWithdrawalAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditWithdrawal(int? id)
        {
            if (id == null) return NotFound();

            var withdraw = await _dashboardService.GetWithdrawalUpdateAsync(id.Value);
            if (withdraw == null) return NotFound();
                                                                                                            
            var selectedDate = DateOnly.FromDateTime(DateTime.Today);

            var priceTiers = await _dashboardService.LoadPriceTierAsync();
            var todaySales = await _dashboardService.GetSaleByDateAsync(selectedDate);
            var withdrawals = await _dashboardService.GetWithdrawalByDateAsync(selectedDate);
            var summary = await _dashboardService.GetDashboardSummaryAsync(selectedDate);

            var totalWithdrawn = withdrawals.Sum(q => q.Amount);

            var model = new DashboardVM
            {
                PriceTiers = priceTiers,
                TodaySales = todaySales,
                TotalWithdrawals = withdrawals,
                SelectedDate = selectedDate,
                TotalCupSold = summary.TotalCupSold,
                DailyIncome = summary.DailyIncome,
                TotalWithdrawnToday = totalWithdrawn,

                IsEditingWithdrawal = true,
                WithdrawalToEdit = withdraw,
            };

            return View(nameof(Index), model); // Render the same Index view


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWithdrawal(int id,  WithdrawalUpdateVM model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _dashboardService.EditWithdrawalAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteWithdraw(int id)
        {
            await _dashboardService.RemoveWithdrawalAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
