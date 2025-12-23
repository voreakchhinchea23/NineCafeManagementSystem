

namespace NineCafeManagementSystem.Web.Models.Reports
{
    public class DailyAmount
    {
        public int Day { get; set; }
        public decimal Amount { get; set; }
        public int? CupsSold { get; set; }
    }
}
