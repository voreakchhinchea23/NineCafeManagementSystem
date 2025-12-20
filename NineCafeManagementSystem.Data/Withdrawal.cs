
namespace NineCafeManagementSystem.Web.Data
{
    public class Withdrawal : BaseEntity
    {
        public DateOnly WithdrawDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public decimal Amount { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
