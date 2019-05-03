using System;
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
        public DbSet<CHSR.Models.Session> Session { get; set; }

        public DbSet<CHSR.Models.ResourcePerson> ResourcePerson { get; set; }

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



            modelBuilder.Entity<Institute>().HasData(new Institute { ID = 1, Name = "University of Dhaka(DU)", Location = "Dhaka" });
            modelBuilder.Entity<Institute>().HasData(new Institute { ID = 2, Name = "Bangladesh University of Professionals(BUP)", Location = "Dhaka" });
            modelBuilder.Entity<Institute>().HasData(new Institute { ID = 3, Name = "Bangladesh Agricultural University(BAU)", Location = "Mymensingh" });
            modelBuilder.Entity<Institute>().HasData(new Institute { ID = 4, Name = "Bangladesh University of Engineering & Technology(BUET)", Location = "Dhaka" });
            modelBuilder.Entity<Institute>().HasData(new Institute { ID = 5, Name = "University of Chittagong(CU)", Location = "Chittagong" });
            modelBuilder.Entity<Institute>().HasData(new Institute { ID = 6, Name = "Jahangirnagar University(JU)", Location = "Savar, Dhaka" });
            modelBuilder.Entity<Institute>().HasData(new Institute { ID = 7, Name = "Islamic University, Bangladesh(IU)", Location = "Kushtia" });
            modelBuilder.Entity<Institute>().HasData(new Institute { ID = 8, Name = "Shahjalal University of Science and Technology(SUST)", Location = "Sylhet" });
            modelBuilder.Entity<Institute>().HasData(new Institute { ID = 9, Name = "Khulna University(KU)", Location = "Khulna" });

            modelBuilder.Entity<Faculty>().HasData(
                new { ID = 1, Name = "Faculty of Arts", InstituteID=1 },
                new { ID = 2, Name = "Faculty of Business Studies", InstituteID = 1 },
                new { ID = 3, Name = "Faculty of Engineering and Technology", InstituteID = 1 });

            modelBuilder.Entity<Department>().HasData(
                new {id=1, Name= "Department of Bangla", FacultyID=1 },
                new { id = 2, Name = "Department of English", FacultyID = 1 },
                new { id = 3, Name = "Department of Persian Language and Literature", FacultyID = 1 },
                new { id = 4, Name = "Department of Management Studies", FacultyID = 2 },
                new { id = 5, Name = "Department of Accounting & Information Systems", FacultyID = 2 },
                new { id = 6, Name = "Department of Electrical and Electronic Engineering", FacultyID = 3 },
                new { id = 7, Name = "Department of Computer Science and Engineering", FacultyID = 3 }
                );

              



        }


    }
}
