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
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasOne(d => d.Department)
                   .WithMany(dep => dep.Doctors)
                   .HasForeignKey(d => d.DepartmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.Appointments)
                   .WithOne(a => a.Doctor)
                   .HasForeignKey(a => a.DoctorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(mr => mr.MedicalRecords)
                   .WithOne(d => d.Doctor)
                   .HasForeignKey(d => d.DoctorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
