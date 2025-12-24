namespace NineCafeManagementSystem.Web.Data
{
    public class Dept : BaseEntity
    {
        public string CustomerName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateOnly DeptDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public bool IsPaid { get; set; } = false;

    }
}
