

namespace NineCafeManagementSystem.Web.Services.PriceTiers
{
    public interface IPriceTierService
    {
        Task<bool> CheckIfPriceTierAlreadyExistsAsync(int price);
        Task CreateAsync(PriceTierCreateVM model);
        Task EditAsync(PriceTierUpdateVM model);
        Task<List<PriceTierReadOnlyVM>> GetAllAsync();
        Task<PriceTierReadOnlyVM?> GetDetailByIdAsync(int id);
        Task<PriceTierUpdateVM?> GetUpdateByIdAsync(int id);
        bool PriceTierExists(int id);
        Task RemoveAsync(int id);

    }
}
