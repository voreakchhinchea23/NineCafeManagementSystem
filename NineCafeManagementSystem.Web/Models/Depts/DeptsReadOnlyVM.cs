namespace NineCafeManagementSystem.Web.Models.Depts
{
    public class DeptsReadOnlyVM
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string CustomerName { get; set; } = string.Empty;
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
        [Display(Name = "Date")]
        public DateOnly DeptDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Display(Name = "Status")]
        public bool IsPaid { get; set; } = false;
    }
}
