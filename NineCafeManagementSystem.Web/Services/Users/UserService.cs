
namespace NineCafeManagementSystem.Web.Services.Users
{
    public class UserService : IUserService
    {
        public Task CreateUserAsync(UserCreateVM model)
        {
            throw new NotImplementedException();
        }

        public Task EditUserAsync(UserUpdateVM model)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserListVM>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserListVM?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserListVM?> GetUserUpdateByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool UserExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
