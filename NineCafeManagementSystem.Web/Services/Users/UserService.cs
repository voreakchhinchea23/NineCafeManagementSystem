namespace NineCafeManagementSystem.Web.Services.Users
{
    public class UserService(
        UserManager<ApplicationUser> _userManager,
        IHttpContextAccessor _httpContextAccessor) : IUserService
    {
        public async Task CreateUserAsync(UserCreateVM model)
        {
            if (await IsPhoneNumberAlreadyExisted(model.PhoneNumber))
            {
                throw new InvalidOperationException("Phone number is already in use by another user.");
            }

            if (await _userManager.FindByEmailAsync(model.Email) == null)
            {
                var newUser = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, Roles.Staff);
                }
                else
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to create user: {errors}");
                }
            }
            else
            {
                throw new InvalidOperationException("User with this email already exists.");
            }
        }

        public async Task EditUserAsync(UserUpdateVM model)
        {
            if (await IsNumberAlreadyUsedByAnotherUserAsync(model.Id, model.PhoneNumber))
            {
                throw new InvalidOperationException("Phone number is already in use by another user.");
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Update failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            // handle if password provided
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                await _userManager.RemovePasswordAsync(user);
                var pwResult = await _userManager.AddPasswordAsync(user, model.Password);
                if (!pwResult.Succeeded)
                {
                    throw new InvalidOperationException($"Password update failed: {string.Join(", ", pwResult.Errors.Select(e => e.Description))}");
                }
            }
        }

        public async Task<List<UserListVM>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var result = new List<UserListVM>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserListVM
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = roles.FirstOrDefault() ?? "None"
                });
            }
            return result.OrderBy(q => q.Role).ToList();
        }

        public async Task<UserListVM?> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            var role = await _userManager.GetRolesAsync(user);
            var userRole = role.FirstOrDefault() ?? "None";

            return new UserListVM
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = userRole
            };
        }

        public async Task<UserUpdateVM?> GetUserUpdateByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            return new UserUpdateVM
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Password = string.Empty
            };
        }

        public async Task<bool> IsPhoneNumberAlreadyExisted(string phoneNumber)
        {
            return await IsNumberAlreadyUsedByAnotherUserAsync(string.Empty, phoneNumber);
        }
        public async Task<bool> IsNumberAlreadyUsedByAnotherUserAsync(string userId, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            var normalized = phoneNumber.Trim();
            return await _userManager.Users
                .AnyAsync(q => q.Id != userId
                && !string.IsNullOrEmpty(q.PhoneNumber)
                && q.PhoneNumber.Trim() == normalized
            );
        }

        public async Task RemoveUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return;
            }

            //prevent deleting yourself(if admin is logged in)
            if (user.Id == _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier))
                throw new InvalidOperationException("You cannot delete your own account.");

            await _userManager.DeleteAsync(user);
        }


    }
}
