namespace NineCafeManagementSystem.Web.Data
{
    public class DailySale : BaseEntity
    {
        public PriceTier? PriceTier { get; set; } 
        public int PriceTierId { get; set; }
        public DateOnly SaleDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public int QuantitySold { get; set; }
    }
}
