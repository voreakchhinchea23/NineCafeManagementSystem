
namespace NineCafeManagementSystem.Web.Services.Users
{
    public interface IUserService
    {
       Task<List<UserListVM>> GetAllUsersAsync();
        Task CreateUserAsync(UserCreateVM model);
        Task EditUserAsync(UserUpdateVM model);
        Task<UserListVM?> GetUserByIdAsync(string id);
        Task<UserUpdateVM?> GetUserUpdateByIdAsync(string id);
        Task RemoveUserAsync(string id);
        Task<bool> IsPhoneNumberAlreadyExisted(string  phoneNumber);
        Task<bool> IsNumberAlreadyUsedByAnotherUserAsync(string userId, string phoneNumber);
    }
}
