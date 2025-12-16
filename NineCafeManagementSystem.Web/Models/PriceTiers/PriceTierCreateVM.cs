namespace NineCafeManagementSystem.Web.Models.PriceTiers
{
    public class PriceTierCreateVM
    {
        [Required]
        [Display(Name = "Price Riel")]
        public int PriceRiel { get; set; }
    }
}
