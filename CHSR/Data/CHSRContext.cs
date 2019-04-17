using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CHSR.Models;

namespace CHSR.Models
{
    public class CHSRContext : DbContext
    {
        public CHSRContext (DbContextOptions<CHSRContext> options)
            : base(options)
        {
        }

        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<AdmissionApplication> AdmissionApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Institute>()
                .HasMany(c => c.Faculties)
                .WithOne(e => e.Institute)
                .IsRequired();


            modelBuilder.Entity<Institute>().HasData(new Institute { ID = 1, Name = "AIUB", Location = "KURIL" });
        }

        public DbSet<CHSR.Models.Session> Session { get; set; }
    }
}
