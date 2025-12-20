
namespace NineCafeManagementSystem.Web.Data
{
    public class PriceTier : BaseEntity
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price Must Be Positive And Greater Than Zero.")]
        public int PriceRiel { get; set; }
    }
}
