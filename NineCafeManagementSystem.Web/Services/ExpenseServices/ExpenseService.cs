


namespace NineCafeManagementSystem.Web.Services.ExpenseServices
{
    public class ExpenseService(ApplicationDbContext _context) : IExpenseService
    {
        public async Task CreateExpenseAsync(ExpenseCreateVM model)
        {
            var entity = new Expense
            {
                Amount = model.Amount,
                ExpenseDate = model.ExpenseDate,
                Description = model.Description,
                Other = model.Other
            };
            _context.Expenses.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditExpenseAsync(ExpenseEditVM model)
        {
            await _context.Expenses
                 .Where(q => q.Id == model.Id)
                 .ExecuteUpdateAsync(s => s
                    .SetProperty(e => e.Amount, model.Amount)
                    .SetProperty(e => e.Description, model.Description)
                    .SetProperty(e => e.Other, model.Other)
                    .SetProperty(e => e.ExpenseDate, model.ExpenseDate)
                 );

        }

        public bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(q => q.Id == id);
        }

        public async Task<List<ExpenseReadOnlyVM>> GetAllExpensesAsync()
        {
            return await _context.Expenses
                .Select(q => new ExpenseReadOnlyVM
                {
                    Id = q.Id,
                    Amount = q.Amount,
                    ExpenseDate = q.ExpenseDate,
                    Description = q.Description,
                    Other = q.Other
                })
                .ToListAsync();
        }

        public async Task<ExpenseReadOnlyVM?> GetExpenseByIdAsync(int? id)
        {
            return await _context.Expenses
                .Where(q => q.Id == id)
                .Select(q => new ExpenseReadOnlyVM
                {
                    Id = q.Id,
                    Amount = q.Amount,
                    ExpenseDate = q.ExpenseDate,
                    Description = q.Description,
                    Other = q.Other
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ExpenseEditVM?> GetExpenseByIdForEditAsync(int? id)
        {
            return await _context.Expenses
                .Where(q => q.Id == id)
                .Select(q => new ExpenseEditVM
                {
                    Id = q.Id,
                    Amount = q.Amount,
                    ExpenseDate = q.ExpenseDate,
                    Description = q.Description,
                    Other = q.Other
                })
                .FirstOrDefaultAsync();
        }

        public async Task RemoveExpenseAysnc(int id)
        {
            var entity = await _context.Expenses.FindAsync(id);

            if(entity != null)
            {
                _context.Expenses.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
