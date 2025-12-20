

namespace NineCafeManagementSystem.Web.Models.Reports
{
    public class MonthlyReportVM
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName => DateTimeFormatInfo.CurrentInfo.GetMonthName(Month);
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Profit => TotalIncome - TotalExpense;


        // in usd
        public const decimal ExchangeRate = 4000; 
        public decimal TotalIncomeUsd => Math.Round(TotalIncome / ExchangeRate, 2);
        public decimal TotalExpensesUsd => Math.Round(TotalExpense / ExchangeRate, 2);
        public decimal ProfitUsd => Math.Round(Profit / ExchangeRate, 2);

        // dropdown in the model
        public List<int> AvailableYears { get; set; } = new();
        public List<int> AvailableMonths { get; set; } = new();
    }
}
