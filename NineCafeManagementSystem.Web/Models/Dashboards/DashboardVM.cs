namespace NineCafeManagementSystem.Web.Models.Dashboards
{
    public class DashboardVM
    {
        public List<PriceTier> PriceTiers { get; set; } = new();
        public Dictionary<int, int> TodaySales { get; set; } = new(); // PriceTierId -> Qty
        public DateOnly SelectedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        // summary
        public int TotalCupSold { get; set; }
        public int DailyIncome { get; set; }
        // withdrawal
        public List<Withdrawal> TotalWithdrawals { get; set; } = new();
        public decimal TotalWithdrawnToday { get; set; }
        public bool IsEditingWithdrawal { get; set; }
        public WithdrawalUpdateVM WithdrawalToEdit { get; set; }

    }
}
