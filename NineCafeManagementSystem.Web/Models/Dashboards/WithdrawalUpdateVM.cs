namespace NineCafeManagementSystem.Web.Models.Dashboards
{
    public class WithdrawalUpdateVM
    {
        [Required]
        public int Id { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "Reason is required.")]
        public string Reason { get; set; } = string.Empty;

        public DateOnly WithdrawDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    }
}
