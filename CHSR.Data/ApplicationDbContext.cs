using CHSR.Data.EntityConfiguration;
using CHSR.Domain;
using CHSR.Domain.Setup;
using CHSR.Domain.UAM;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CHSR.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {


        public DbSet<AdmissionApplication> AdmissionApplications { get; set; }
        public DbSet<Institute> Institutes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdmissionApplicationConfiguration());
            modelBuilder.ApplyConfiguration(new InstituteConfiguration());

            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
