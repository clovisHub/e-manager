using BeesApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentitySample.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, BeesApp.Migrations.Configuration>("DefaultConnection"));
            
        }

        public DbSet<DriverModel> Drivers { get; set; }

        public DbSet<CashierModel> Cashiers { get; set; }

        public DbSet<ManagerAdmiModel> Managers { get; set; }
        
        public DbSet<OnlineOrderModel> Orders { get; set; }

        public DbSet<BusinessOptionModel> BusinessOption { get; set; }

        public DbSet<CultivatorModel> Cultivators { get; set; }

        public DbSet<GlobalReportModel> Global { get; set; }

        public DbSet<DeliveriesModel> Deliveries { get; set; }

        public DbSet<SavedReportModel> SavedReportModel { set; get; }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<BeesApp.Models.Shipping> Shippings { get; set; }

    }
}