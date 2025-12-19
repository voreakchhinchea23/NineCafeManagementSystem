
namespace NineCafeManagementSystem.Web.Data
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
        public string? ContactInfo { get; set; }
        public string? Address { get; set; }
        public string? ProductType { get; set; }
    }
}
