using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Dtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public int DoctorId { get; set; }
        public string? Doctor { get; set; }
        public int PatientId { get; set; }
        public string? Patient { get; set; }
    }
}
