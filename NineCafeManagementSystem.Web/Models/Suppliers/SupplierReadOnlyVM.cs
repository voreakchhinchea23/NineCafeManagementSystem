namespace NineCafeManagementSystem.Web.Models.Suppliers
{
    public class SupplierReadOnlyVM
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Contact Infomation")]
        public string? ContactInfo { get; set; }
        [Display(Name = "Address")]
        public string? Address { get; set; }
        [Display(Name = "Product Type")]
        public string? ProductType { get; set; }

    }
}
