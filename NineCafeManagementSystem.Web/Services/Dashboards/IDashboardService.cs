


namespace NineCafeManagementSystem.Web.Services.Dashboards
{
    public interface IDashboardService
    {
        Task EditSaleAsync(int priceTierId, int quantity);
        Task EditWithdrawalAsync(WithdrawalUpdateVM model);
        Task<DashboardSummaryVM> GetDashboardSummaryAsync(DateOnly date);
        Task<Dictionary<int, int>> GetSaleByDateAsync(DateOnly date);
        Task<List<Withdrawal>> GetWithdrawalByDateAsync(DateOnly date);
        Task<WithdrawalUpdateVM?> GetWithdrawalUpdateAsync(int id);
        Task<List<PriceTier>> LoadPriceTierAsync();
        Task RecordSaleAsync(int priceRiel);
        Task RecordWithdrawalAsync(WithdrawalCreateVM model);
        Task RemoveWithdrawalAsync(int id);
    }
}
