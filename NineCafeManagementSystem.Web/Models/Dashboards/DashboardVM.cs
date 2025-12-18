namespace NineCafeManagementSystem.Web.Models.Dashboards
{
    public class DashboardVM
    {
        public List<PriceTier> PriceTiers { get; set; } = new();
        public Dictionary<int, int> TodaySales { get; set; } = new(); // PriceTierId -> Qty
        public DateOnly SelectedDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    }
}
