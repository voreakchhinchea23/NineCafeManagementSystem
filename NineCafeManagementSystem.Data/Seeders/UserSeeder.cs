namespace NineCafeManagementSystem.Web.Data.Seeders
{
    public class UserSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider sp)
        {
            var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();

            await CreateUserWithRole(
                userManager,
                "admin@ninecafe.com",
                "P@ssword1",
                "Default",
                "Admin",
                Roles.Admin);

        }

        private static async Task CreateUserWithRole(
            UserManager<ApplicationUser> userManager,
            string email,
            string password,
            string firstName,
            string lastName,
            string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    FirstName = firstName,
                    LastName = lastName
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"Failed creating user with email {user.Email}. Error: {string.Join(",", result.Errors)}");
                }
            }
        }
    }
}
