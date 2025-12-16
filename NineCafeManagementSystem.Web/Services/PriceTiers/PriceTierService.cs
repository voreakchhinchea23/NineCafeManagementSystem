namespace NineCafeManagementSystem.Web.Services.PriceTiers
{
    public class PriceTierService(ApplicationDbContext _context) : IPriceTierService
    {
        public async Task CreateAsync(PriceTierCreateVM model)
        {
            var entity = new PriceTier
            {
                PriceRiel = model.PriceRiel,
            };

            await _context.PriceTiers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(PriceTierUpdateVM model)
        {
            await _context.PriceTiers
                .Where(q => q.Id == model.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(e => e.PriceRiel , model.PriceRiel)
                );
        }

        public async Task<List<PriceTierReadOnlyVM>> GetAllAsync()
        {
            var data = await _context.PriceTiers
                .Select(pt => new PriceTierReadOnlyVM
                {
                    Id = pt.Id,
                    PriceRiel = pt.PriceRiel
                })
                .ToListAsync();

            return data;
        }

        public async Task<PriceTierUpdateVM?> GetUpdateByIdAsync(int id)
        {
            return await _context.PriceTiers
                .Where(q => q.Id == id)
                .Select(q => new PriceTierUpdateVM
                {
                    Id = q.Id,
                    PriceRiel = q.PriceRiel
                }).FirstOrDefaultAsync();
        }
        public async Task<PriceTierReadOnlyVM?> GetDetailByIdAsync(int id)
        {
            return await _context.PriceTiers
                .Where(q => q.Id == id)
                .Select(q => new PriceTierReadOnlyVM
                {
                    Id = q.Id,
                    PriceRiel = q.PriceRiel
                }).FirstOrDefaultAsync();
        }

        public bool PriceTierExists(int id)
        {
            return _context.PriceTiers.Any(q => q.Id == id);
        }
        public async Task<bool> CheckIfPriceTierAlreadyExistsAsync(int price)
        {
            return await _context.PriceTiers.AnyAsync(q => q.PriceRiel == price);
        }

        public async Task RemoveAsync(int id)
        {
            var data = await _context.PriceTiers.FindAsync(id);

            if(data != null)
            {
                _context.PriceTiers.Remove(data);
                await _context.SaveChangesAsync();
            }
        }
    }
}
