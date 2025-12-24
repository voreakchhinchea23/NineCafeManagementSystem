
namespace NineCafeManagementSystem.Web.Services.Depts
{
    public class DeptService(ApplicationDbContext _context) : IDeptService
    {
        public async Task CreateDeptAsync(DeptsCreateVM model)
        {
            _context.Depts.Add(new Dept
            {
                CustomerName = model.CustomerName,
                Amount = model.Amount,
                DeptDate = model.DeptDate,
                IsPaid = false // always set to false when creating a new dept
            });
            await _context.SaveChangesAsync();

        }

        public bool DebtExists(int id)
        {
            return _context.Depts.Any(q => q.Id == id);
        }

        public async Task<List<DeptsReadOnlyVM>> GetAllDeptsAsync()
        {
            return await _context.Depts
                .Select(q => new DeptsReadOnlyVM
                {
                    Id = q.Id,
                    CustomerName = q.CustomerName,
                    Amount = q.Amount,
                    DeptDate = q.DeptDate,
                    IsPaid = q.IsPaid
                })
                .ToListAsync();
        }

        public async Task<DeptsReadOnlyVM?> GetDeptByIdAsync(int id)
        {
            return await _context.Depts
                .Where(q => q.Id == id)
                .Select(q => new DeptsReadOnlyVM
                {
                    Id = q.Id,
                    CustomerName = q.CustomerName,
                    Amount = q.Amount,
                    DeptDate = q.DeptDate,
                    IsPaid = q.IsPaid
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DeptsUpdateVM?> GetDeptByIdForEditAsync(int id)
        {
            return await _context.Depts
                .Where(q => q.Id == id)
                .Select(q => new DeptsUpdateVM
                {
                    Id = q.Id,
                    CustomerName = q.CustomerName,
                    Amount = q.Amount,
                    DeptDate = q.DeptDate,
                    IsPaid = q.IsPaid
                })
                .FirstOrDefaultAsync();
        }

       
        public async Task RemoveDeptAsync(int id)
        {
            await _context.Depts
                .Where(q => q.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task UpdateDeptAsync(DeptsUpdateVM model)
        {
            await _context.Depts
               .Where(q => q.Id == model.Id)
               .ExecuteUpdateAsync(s => s
                   .SetProperty(e => e.CustomerName, model.CustomerName)
                   .SetProperty(e => e.Amount, model.Amount)
                   .SetProperty(e => e.DeptDate, model.DeptDate)
                   .SetProperty(e => e.IsPaid, model.IsPaid)
               );
        }

        public Task<bool> CustomerNameExistsAsync(string customerName)
        {
            return _context.Depts.AnyAsync(q => q.CustomerName == customerName);
        }

        public async Task<bool> CustomerNameAlreadyInUseByAnotherAsync(int id, string customername)
        {
            if (string.IsNullOrWhiteSpace(customername))
            {
                return false;
            }
            var normalized = customername.Trim();
            return await _context.Depts
                .AnyAsync(q => q.Id != id
                && !string.IsNullOrEmpty(q.CustomerName)
                && q.CustomerName.Trim() == normalized);
        }
    }
}
