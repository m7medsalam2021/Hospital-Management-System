using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Specifications
{
    public class DoctorWithFiltrationForCountSpecification : BaseSpecification<Doctor>
    {
        public DoctorWithFiltrationForCountSpecification(DoctorSpecParams doctorSpecParams) : base(P =>
                (!doctorSpecParams.AppointmentId.HasValue || P.AppointmentsId == doctorSpecParams.AppointmentId &&
                !doctorSpecParams.DepartmentId.HasValue || P.DepartmentId == doctorSpecParams.DepartmentId))

        {
            
        }
    }
}
