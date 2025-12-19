namespace NineCafeManagementSystem.Web.Models.Expenses
{
    public class ExpenseReadOnlyVM
    {
        public int Id { get; set; }
        [Display(Name = "Date")]
        public DateOnly ExpenseDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Other")]
        public string? Other { get; set; }
    }
}
