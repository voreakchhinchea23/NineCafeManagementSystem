
namespace NineCafeManagementSystem.Web.Services.Dashboards
{
    public class DashboardService(ApplicationDbContext _context) : IDashboardService
    {
        public async Task<List<PriceTier>> LoadPriceTierAsync()
        {
            return await _context.PriceTiers.OrderBy(p => p.PriceRiel).ToListAsync();
        }

        public async Task RecordSaleAsync(int priceRiel)
        {
           var today = DateOnly.FromDateTime(DateTime.Today);

            var priceTier = await _context.PriceTiers.FirstOrDefaultAsync(q => q.PriceRiel == priceRiel);
            if (priceTier == null) throw new ArgumentException("Price not found");

            var existing = await _context.DailySales.FirstOrDefaultAsync(
                q => q.PriceTierId == priceTier.Id &&
                q.SaleDate == today
                );

            if (existing != null)
            {
                existing.QuantitySold++;
            }
            else
            {
                _context.DailySales.Add(new DailySale
                {
                    PriceTierId = priceTier.Id,
                    SaleDate = today,
                    QuantitySold = 1
                });
            }

            await _context.SaveChangesAsync();

        }

        public async Task<Dictionary<int, int>> GetSaleByDateAsync(DateOnly date)
        {
            return await _context.DailySales
                .Where(q => q.SaleDate == date)
                .ToDictionaryAsync(q => q.PriceTierId, q => q.QuantitySold);
        }
    }
}
