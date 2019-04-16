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
        public DbSet<AdmissionApplication> AdmissionApplications { get; set; }
    }
}
