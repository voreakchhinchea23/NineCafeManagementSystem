
namespace NineCafeManagementSystem.Web.Services.Reports
{
    public class ReportService(ApplicationDbContext _context) : IReportService
    {
        public async Task<byte[]> GenerateMonthlyReportExcelAsync(int year, int month)
        {
            // Get all data
            var totalIncome = await GetTotalIncomeAsync(year, month);
            var totalExpense = await GetTotalExpenseAsync(year, month);
            var dailyIncome = await GetDailyIncomeAsync(year, month);
            var dailyExpense = await GetDailyExpenseAsync(year, month);
            

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Monthly Report");

            // title
            worksheet.Cell(1, 1).Value = $"Monthly Report - {DateTimeFormatInfo.CurrentInfo.GetMonthName(month)} {year}";
            worksheet.Cell(1,1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 14;

            // summary 
            worksheet.Cell(3, 1).Value = "Total Income";
            worksheet.Cell(3, 2).Value = totalIncome;
            worksheet.Cell(3, 2).Style.NumberFormat.Format = "#,##0 ៛";

            worksheet.Cell(4, 1).Value = "Total Expense";
            worksheet.Cell(4, 2).Value = totalExpense;
            worksheet.Cell(4, 2).Style.NumberFormat.Format = "#,##0 ៛";

            worksheet.Cell(5, 1).Value = "Total Profit";
            worksheet.Cell(5, 2).Value = totalIncome - totalExpense;
            worksheet.Cell(5, 2).Style.NumberFormat.Format = "#,##0 ៛";

            // daily sales
            worksheet.Cell(7, 1).Value = "Daily Sales";
            worksheet.Cell(7, 1).Style.Font.Bold = true;
            worksheet.Cell(8, 1).Value = "Date";
            worksheet.Cell(8, 2).Value = "Income (Riel)";
            worksheet.Cell(8, 3).Value = "Cups Sold"; 

            var row = 9;
            foreach(var day in dailyIncome)
            {
                worksheet.Cell(row, 1).Value = new DateTime(year, month, day.Day);
                worksheet.Cell(row, 1).Style.DateFormat.Format = "dd/MM/yyyy";
                worksheet.Cell(row, 2).Value = day.Amount;
                worksheet.Cell(row, 2).Style.NumberFormat.Format = "#,##0 ៛";
                worksheet.Cell(row, 3).Value = day.CupsSold;
                row++;
            }

            // Daily expenses
            row += 2;
            worksheet.Cell(row, 1).Value = "Daily Expense";
            worksheet.Cell(row, 1).Style.Font.Bold = true;
            row++;
            worksheet.Cell(row, 1).Value = "Date";
            worksheet.Cell(row, 2).Value = "Expense (Riel)";
            row++;

            foreach (var day in dailyExpense)
            {
                worksheet.Cell(row, 1).Value = new DateTime(year, month, day.Day);
                worksheet.Cell(row, 1).Style.DateFormat.Format = "dd/MM/yyyy";
                worksheet.Cell(row, 2).Value = day.Amount;
                worksheet.Cell(row, 2).Style.NumberFormat.Format = "#,##0 ៛";
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();

        }



        public async Task<MonthlyReportVM> GetMonthlyReportAsync(int year, int month)
        {
            var totalIncome = await GetTotalIncomeAsync(year, month);
            var totalExpenses = await GetTotalExpenseAsync(year, month);

            return new MonthlyReportVM
            {
                Year = year,
                Month = month,
                TotalIncome = totalIncome,
                TotalExpense = totalExpenses
            };
        }

        private async Task<List<DailyAmount>> GetDailyExpenseAsync(int year, int month)
        {
            return await _context.Expenses
                .Where(e => e.ExpenseDate.Year == year && e.ExpenseDate.Month == month)
                .GroupBy(e => e.ExpenseDate.Day)
                .Select(g => new DailyAmount
                {
                    Day = g.Key,
                    Amount = g.Sum(x => x.Amount)
                })
                .OrderBy(x => x.Day)
                .ToListAsync();
        }
       

        private async Task<List<DailyAmount>> GetDailyIncomeAsync(int year, int month)
        {
            return await _context.DailySales
                .Where(ds => ds.SaleDate.Year == year && ds.SaleDate.Month == month)
                .GroupBy(ds => ds.SaleDate.Day)
                .Select(g => new DailyAmount
                {
                    Day = g.Key,
                    Amount = g.Sum(x => (decimal)(x.PriceTier.PriceRiel * x.QuantitySold)),
                    CupsSold = g.Sum(x => x.QuantitySold)
                })
                .OrderBy(x => x.Day)
                .ToListAsync();
        }

        private async Task<decimal> GetTotalExpenseAsync(int year, int month)
        {
            return await _context.Expenses
                .Where(e => e.ExpenseDate.Year == year && e.ExpenseDate.Month == month)
                .SumAsync(e => e.Amount);
        }

        private async Task<decimal> GetTotalIncomeAsync(int year, int month)
        {
            return await _context.DailySales
                .Where(ds => ds.SaleDate.Year == year && ds.SaleDate.Month == month)
                .SumAsync(ds => (decimal)(ds.PriceTier.PriceRiel * ds.QuantitySold));
        }
    }
}
