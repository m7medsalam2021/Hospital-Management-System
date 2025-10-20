using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Specifications
{
    public class AppiontmentWithDoctorAndPatientSpecification : BaseSpecification<Appointment>
    {
        public AppiontmentWithDoctorAndPatientSpecification()
        {
            Include.Add(d => d.Doctor);
            Include.Add(d => d.Patient);
        }
        public AppiontmentWithDoctorAndPatientSpecification(int id) : base(i => i.Id == id)
        {
            Include.Add(d => d.Doctor);
            Include.Add(d => d.Patient);
        }
    }
}
