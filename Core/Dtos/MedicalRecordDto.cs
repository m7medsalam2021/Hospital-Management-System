using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hospital.Core.Dtos
{
    public class MedicalRecordDto
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        [JsonIgnore]
        public int PatientId { get; set; }
        public string? Patient { get; set; }
        [JsonIgnore]
        public int DoctorId { get; set; }
        public string? Doctor { get; set; }
    }
}
