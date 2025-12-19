


namespace NineCafeManagementSystem.Web.Services.Dashboards
{
    public interface IDashboardService
    {
        Task EditSaleAsync(int priceTierId, int quantity);
        Task<DashboardSummaryVM> GetDashboardSummaryAsync(DateOnly date);
        Task<Dictionary<int, int>> GetSaleByDateAsync(DateOnly date);
        Task<List<Withdrawal>> GetWithdrawalByDateAsync(DateOnly date);
        Task<List<PriceTier>> LoadPriceTierAsync();
        Task RecordSaleAsync(int priceRiel);
        Task RecordWithdrawalAsync(WithdrawalCreateVM model);
    }
}
