using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hospital.Core.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; } 
        public string Specialty { get; set; }

        public int AppointmentsId { get; set; }
        public ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public ICollection<MedicalRecord>? MedicalRecords { get; set; } = new List<MedicalRecord>();

    }
}
