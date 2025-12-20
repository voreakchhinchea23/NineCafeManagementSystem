namespace NineCafeManagementSystem.Web.Services.Reports
{
    public class ReportService(ApplicationDbContext _context) : IReportService
    {
        public async Task<MonthlyReportVM> GetMonthlyReportAsync(int year, int month)
        {
            // get total income
            var totalIncome = await _context.DailySales
                .Where(i => i.SaleDate.Year == year && i.SaleDate.Month == month)
                .SumAsync(i => (decimal)(i.PriceTier.PriceRiel * i.QuantitySold));

            // get total expense
            var totalExpense = await _context.Expenses
                .Where(e => e.ExpenseDate.Year == year && e.ExpenseDate.Month == month)
                .SumAsync(e => e.Amount);

            return new MonthlyReportVM
            {
                Year = year,
                Month = month,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense
            };
        }
    }
}
