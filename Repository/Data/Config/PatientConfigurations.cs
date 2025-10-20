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
    public class PatientConfigurations : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasMany(p => p.Appointments)
               .WithOne(a => a.Patient)
               .HasForeignKey(a => a.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(mr => mr.MedicalRecords)
                .WithOne(p => p.Patient)
                .HasForeignKey(mr => mr.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
