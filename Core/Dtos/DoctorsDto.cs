using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hospital.Core.Dtos
{
    public class DoctorsDto
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string Specialty { get; set; }

        public int AppointmentId { get; set; }
        public string? Appointments { get; set; }

        public int DepartmentId { get; set; }
        public string? Departments { get; set; }


    }
}
