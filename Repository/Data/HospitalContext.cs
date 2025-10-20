using Hospital.Core.Entities;
using Hospital.Repository.Data.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repository.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent Api 
            //modelBuilder.ApplyConfiguration(new PatientConfigurations());

            ///apply all configurations for the project
            ///[reflection] you can dynamically discover information about types (classes, interfaces, structs), methods, fields, properties, events, and more within an assembly.
            /// get all classes which implements IEntityTypeConfigurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }
    }
}
