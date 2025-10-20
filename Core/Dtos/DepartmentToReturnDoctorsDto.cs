using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Dtos
{
    public class DepartmentToReturnDoctorsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<DoctorsDto>? Doctors { get; set; } = new List<DoctorsDto>();
    }
}
