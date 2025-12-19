namespace NineCafeManagementSystem.Web.Data
{
    public class Expense : BaseEntity
    {
        public DateOnly ExpenseDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public decimal Amount { get; set; } // Can be USD or KHR
        public string Description { get; set; }
        public string? Other { get; set; }

    }
}
