namespace NineCafeManagementSystem.Web.Data.Seeders
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider sp)
        {
            var roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { Roles.Admin, Roles.Staff };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

        }
    }
}
