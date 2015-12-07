using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace RISH.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DbConnection", throwIfV1Schema: false)
		{
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Cambia el nombre de las tablas
			modelBuilder.Entity<ApplicationUser>().ToTable("Users");
			modelBuilder.Entity<IdentityRole>().ToTable("Roles");
			modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
			modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
			modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
		}

		public DbSet<Hardware> Hardwares { get; set; }
		public DbSet<HardwareType> HardwareTypes { get; set; }
		public DbSet<HardwareStatus> HardwareStatuses { get; set; }
		public DbSet<Software> Softwares { get; set; }
		public DbSet<Vendor> Vendors { get; set; }
		public DbSet<LicenseType> LicenseTypes { get; set; }
		public DbSet<SoftwareArchitecture> SoftwareArchitectures { get; set; }
		public DbSet<Provider> Providers { get; set; }
		public DbSet<InstalledSoftware> InstalledSoftwares { get; set; }
        public DbSet<Location> Locations { get; set; }

        //public System.Data.Entity.DbSet<RISH.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}
