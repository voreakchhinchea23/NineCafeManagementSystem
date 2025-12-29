
namespace NineCafeManagementSystem.Web.Services.Depts
{
    public interface IDeptService
    {
        Task CreateDeptAsync(DeptsCreateVM model);
        Task UpdateDeptAsync(DeptsUpdateVM model);
        Task<DeptsUpdateVM?> GetDeptByIdForEditAsync(int id);
        Task<DeptsReadOnlyVM?> GetDeptByIdAsync(int id);
        Task RemoveDeptAsync(int id);
        bool DebtExists(int id);
        Task<bool> CustomerNameExistsAsync(string customerName);
        Task<bool> CustomerNameAlreadyInUseByAnotherAsync(int? id, string customername);
        Task<List<DeptsReadOnlyVM>> GetPaidDeptsAsync();
        
        Task MarkDeptAsPaidAsync(int id);
        Task<List<DeptsReadOnlyVM>> GetUnpaidDebtsAsync();
    }
}
