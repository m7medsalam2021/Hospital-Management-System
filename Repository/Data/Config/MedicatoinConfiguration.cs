using Hospital.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repository.Data.Config
{
    public class MedicatoinConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.HasMany(p => p.PrescriptionDetails)
                .WithOne(p => p.Medication)
                .HasForeignKey(a=> a.MedicationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
