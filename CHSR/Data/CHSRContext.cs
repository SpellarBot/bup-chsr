using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CHSR.Models
{
    public class CHSRContext : DbContext
    {
        public CHSRContext (DbContextOptions<CHSRContext> options)
            : base(options)
        {
        }

        public DbSet<CHSR.Models.Institute> Institute { get; set; }
        public DbSet<CHSR.Models.Faculty> Faculty { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Institute>()
                .HasMany(c => c.Faculties)
                .WithOne(e => e.Institute);
        }

    }
}
