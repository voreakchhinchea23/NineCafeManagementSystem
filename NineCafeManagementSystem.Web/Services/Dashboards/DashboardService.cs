
using NineCafeManagementSystem.Web.Data;

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

        public async Task EditSaleAsync(int priceTierId, int quantity)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var existing = await _context.DailySales.FirstOrDefaultAsync(
                 q => q.PriceTierId == priceTierId &&
                 q.SaleDate == today
                 );

            if (existing != null)
            {
                if (quantity > 0)
                {
                    existing.QuantitySold = quantity;
                }
                else
                {
                    _context.DailySales.Remove(existing);
                }
            }
            else if (quantity > 0)
            {
                _context.DailySales.Add(new DailySale
                {
                    PriceTierId = priceTierId,
                    QuantitySold = quantity,
                    SaleDate = today
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task<DashboardSummaryVM> GetDashboardSummaryAsync(DateOnly date)
        {
            var priceTiers = await _context.PriceTiers.ToListAsync();

            var sales = await _context.DailySales
                .Where(q => q.SaleDate == date)
                .ToDictionaryAsync(q => q.PriceTierId, q => q.QuantitySold);

            int totalCups = 0;
            int dailyIncome = 0;

            foreach (var priceTier in priceTiers)
            {
                if (sales.TryGetValue(priceTier.Id, out var qty))
                {
                    totalCups += qty;
                    dailyIncome += qty * priceTier.PriceRiel;
                }
            }

            return new DashboardSummaryVM
            {
                DailyIncome = dailyIncome,
                TotalCupSold = totalCups
            };
        }

        public async Task RecordWithdrawalAsync(WithdrawalCreateVM model)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var entity = new Withdrawal
            {
                Amount = model.Amount,
                Reason = model.Reason,
                WithdrawDate = today,
            };
            _context.Withdrawals.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Withdrawal>> GetWithdrawalByDateAsync(DateOnly date)
        {
            return await _context.Withdrawals
                .Where(q => q.WithdrawDate == date)
                .OrderByDescending(q => q.Id)
                .ToListAsync();
        }

        public async Task<WithdrawalUpdateVM?> GetWithdrawalUpdateAsync(int id)
        {
            return await _context.Withdrawals
                .Where(q => q.Id == id)
                .Select(q => new WithdrawalUpdateVM
                {
                    Id = q.Id,
                    Amount = q.Amount,
                    Reason = q.Reason,
                    WithdrawDate = q.WithdrawDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task EditWithdrawalAsync(WithdrawalUpdateVM model)
        {
            await _context.Withdrawals
                .Where(q => q.Id == model.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(e => e.Amount, model.Amount)
                    .SetProperty(e => e.Reason, model.Reason)
                );
        }

        public async Task RemoveWithdrawalAsync(int id)
        {
            var data = await _context.Withdrawals.FindAsync(id);
            if (data != null)
            {
                _context.Withdrawals.Remove(data);
                await _context.SaveChangesAsync();
            }


        }
    }
}