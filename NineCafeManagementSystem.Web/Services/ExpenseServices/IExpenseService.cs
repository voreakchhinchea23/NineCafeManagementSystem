

namespace NineCafeManagementSystem.Web.Services.ExpenseServices
{
    public interface IExpenseService
    {
        Task CreateExpenseAsync(ExpenseCreateVM model);
        Task EditExpenseAsync(ExpenseEditVM model);
        Task<List<ExpenseReadOnlyVM>> GetAllExpensesAsync();
        Task<ExpenseReadOnlyVM?> GetExpenseByIdAsync(int? id);
        Task<ExpenseEditVM?> GetExpenseByIdForEditAsync(int? id);
        Task<List<ExpenseReadOnlyVM>> GetExpensesByMonthAsync(int year, int month);
        Task RemoveExpenseAysnc(int id);
        public bool ExpenseExists(int id);
    }
}
