namespace NineCafeManagementSystem.Web.Models.Expenses
{
    public class ExpenseCreateVM
    {
        [Display(Name = "Date")]
        public DateOnly ExpenseDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Display(Name = "Amount"), Required]
        [Range(1, double.MaxValue, ErrorMessage = "The amount cannot be less than one.")]
        public decimal Amount { get; set; }
        [Display(Name = "Description"), Required]
        [StringLength(255, ErrorMessage = "Please enter a short description.")]
        public string Description { get; set; }
        [Display(Name = "Other")]
        public string? Other { get; set; }
    }
}
