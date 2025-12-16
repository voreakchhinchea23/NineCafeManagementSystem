namespace NineCafeManagementSystem.Web.Models.PriceTiers
{
    public class PriceTierUpdateVM : BasePriceTierVM
    {
        [Required]
        [Display(Name = "Price Riel")]
        public int PriceRiel { get; set; }
    }
}
