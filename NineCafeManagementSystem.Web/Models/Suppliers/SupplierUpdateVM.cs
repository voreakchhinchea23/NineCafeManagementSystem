namespace NineCafeManagementSystem.Web.Models.Suppliers
{
    public class SupplierUpdateVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name"), StringLength(100)]
        public string Name { get; set; }
        [Display(Name = "Contact Information"), StringLength(100)]
        public string? ContactInfo { get; set; }
        [Display(Name = "Address"), StringLength(255)]
        public string? Address { get; set; }
        [Display(Name = "Product Type"), StringLength(100)]
        public string? ProductType { get; set; }

    }
}
