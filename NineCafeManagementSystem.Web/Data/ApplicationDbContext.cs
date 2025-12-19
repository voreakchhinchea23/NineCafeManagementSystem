namespace NineCafeManagementSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<PriceTier> PriceTiers { get; set; }
        public DbSet<DailySale> DailySales { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
    }
}
