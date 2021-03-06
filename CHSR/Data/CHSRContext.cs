﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CHSR.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CHSR.Models
{
    public class CHSRContext : IdentityDbContext
    {
        public CHSRContext(DbContextOptions<CHSRContext> options)
            : base(options)
        {
        }

        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<SubSpecialization> SubSpecializations { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<AdmissionApplication> AdmissionApplications { get; set; }
        public DbSet<ApplicationAttachment> ApplicationAttachments { get; set; }
        public DbSet<ResearchInterest> ResearchInterests { get; set; }
        public DbSet<ResearchArea> ResearchAreas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Institute>()
                    .HasMany(c => c.Faculties)
                    .WithOne(e => e.Institute)
                    .IsRequired();

            modelBuilder.Entity<Faculty>()
                    .HasMany(c => c.Departments)
                    .WithOne(e => e.Faculty)
                    .IsRequired();

            modelBuilder.Entity<Specialization>()
                   .HasMany(c => c.SubSpecializations)
                   .WithOne(e => e.Specialization)
                   .IsRequired();

            modelBuilder.Entity<ResearchArea>()
                    .HasKey(ra => new { ra.ResourcePersonId, ra.ResearchInterestId });

            modelBuilder.Entity<ResearchArea>()
                    .HasOne(ra => ra.ResourcePerson)
                    .WithMany(rp => rp.ResearchAreas)
                    .HasForeignKey(ra => ra.ResourcePersonId);
            modelBuilder.Entity<ResearchArea>()
                    .HasOne(ra => ra.ResearchInterest)
                    .WithMany(ri => ri.ResearchAreas)
                    .HasForeignKey(ra => ra.ResearchInterestId);

            modelBuilder.Entity<Institute>().HasData(new Institute { ID = 1, Name = "AIUB", Location = "KURIL" });
        }

        public DbSet<CHSR.Models.Session> Session { get; set; }

        public DbSet<CHSR.Models.ResourcePerson> ResourcePerson { get; set; }
    }
}
