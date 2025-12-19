

namespace NineCafeManagementSystem.Web.Services.Suppliers
{
    public interface ISupplierService
    {
        Task CreateSupplierAsync(SupplierCreateVM model);
        Task EditSupplierAsync(SupplierUpdateVM model);
        Task<List<SupplierReadOnlyVM>> GetAllSuppliersAsync();
        Task<SupplierReadOnlyVM> GetSupplierByIdAsync(int id);
        Task<SupplierUpdateVM> GetSupplierByIdForEditAsync(int id);
        bool SupplierExists(int id);
        Task<bool> PhoneNumberExistsAsync(string contact);
        Task RemoveSupplierAysnc(int id);
        Task<bool> IsNumberAlreadyUsedByAnotherSupplierAsync(int supplierId, string phoneNumber);
    }
}
