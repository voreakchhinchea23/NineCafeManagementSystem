
namespace NineCafeManagementSystem.Web.Services.Dashboards
{
    public interface IDashboardService
    {
        Task<Dictionary<int, int>> GetSaleByDateAsync(DateOnly date);
        Task<List<PriceTier>> LoadPriceTierAsync();
        Task RecordSaleAsync(int priceRiel);

    }
}
