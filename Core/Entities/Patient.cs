using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Entities
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone {  get; set; } 
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }

        public int? AppointmentsId { get; set; }
        public ICollection<Appointment>? Appointments { get; set; } = new List<Appointment>();

        public int? MedicalRecordsId { get; set; }
        public ICollection<MedicalRecord>? MedicalRecords { get; set; } = new List<MedicalRecord>();
    }
}
