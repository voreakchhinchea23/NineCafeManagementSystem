namespace NineCafeManagementSystem.Web.Models.Depts
{
    public class DeptsUpdateVM
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        [Display(Name = "Amount")]
        [Required, Range(0.01, double.MaxValue, ErrorMessage = "The amount cannot be less than zero.")]
        public decimal Amount { get; set; }
        [Display(Name = "Date")]
        [Required]
        public DateOnly DeptDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Display(Name = "Status")]
        public bool IsPaid { get; set; } = false;
    }
}
