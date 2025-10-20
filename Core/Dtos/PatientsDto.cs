using Hospital.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HospitalManagementSystem.Dtos
{
    public class PatientsDto
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }
        [JsonIgnore]
        public int AppointmentsId { get; set; }
        public string? Appointments { get; set; }

        [JsonIgnore]
        public int MedicalRecordsId { get; set; }
        public string? MedicalRecords { get; set; }

    }
}
