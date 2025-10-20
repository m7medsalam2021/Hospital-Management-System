using Hospital.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Repository.Data.Config
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.HasMany(m => m.PrescriptionDetails)
                .WithOne(m => m.MedicalRecord)
                .HasForeignKey(i => i.MedicalRecordId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
