namespace NineCafeManagementSystem.Web.Models.PriceTiers
{
    public class PriceTierReadOnlyVM : BasePriceTierVM
    {
        [Display(Name = "Price Riel")]
        public int PriceRiel { get; set; }
    }
}
