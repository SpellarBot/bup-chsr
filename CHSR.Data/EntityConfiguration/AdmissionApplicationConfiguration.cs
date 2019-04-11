using CHSR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CHSR.Data.EntityConfiguration
{
    public class AdmissionApplicationConfiguration : IEntityTypeConfiguration<AdmissionApplication>
    {
        public void Configure(EntityTypeBuilder<AdmissionApplication> builder)
        {
            builder.HasKey(entity => entity.Id);
        }
    }
}
