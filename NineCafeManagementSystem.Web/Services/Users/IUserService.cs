
namespace NineCafeManagementSystem.Web.Services.Users
{
    public interface IUserService
    {
       Task<List<UserListVM>> GetAllUsersAsync();
        Task CreateUserAsync(UserCreateVM model);
        Task EditUserAsync(UserUpdateVM model);
        Task<UserListVM?> GetUserByIdAsync(int id);
        Task<UserListVM?> GetUserUpdateByIdAsync(int id);
        bool UserExists(int id);
        Task RemoveUserAsync(int id);

    }
}
