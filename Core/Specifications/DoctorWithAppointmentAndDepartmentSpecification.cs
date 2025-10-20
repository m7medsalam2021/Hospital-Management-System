using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Specifications
{
    public class DoctorWithAppointmentAndDepartmentSpecification : BaseSpecification<Doctor>
    {
        public DoctorWithAppointmentAndDepartmentSpecification(DoctorSpecParams doctorSpecParams) :
             base(P =>
                (!doctorSpecParams.AppointmentId.HasValue || P.AppointmentsId == doctorSpecParams.AppointmentId &&
                !doctorSpecParams.DepartmentId.HasValue || P.DepartmentId == doctorSpecParams.DepartmentId))

        {
            Include.Add(a => a.Appointments);
            Include.Add(d => d.Department);

            if (!string.IsNullOrEmpty(doctorSpecParams.Sort))
            {
                switch (doctorSpecParams.Sort)
                {
                    case "NameDoctorAsc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "NameDoctorDesc":
                        AddOrderByDesc(p => p.Name);
                        break;
                    default:
                        AddOrderBy(p => p.Id);
                        break;
                }
            }

            ApplyPagination(doctorSpecParams.PageZize * (doctorSpecParams.PageIndex - 1), doctorSpecParams.PageZize);

        }

        public DoctorWithAppointmentAndDepartmentSpecification(int id) : base(i => i.Id == id)
        {
            Include.Add(a => a.Appointments);
            Include.Add(d => d.Department);
        }
        public DoctorWithAppointmentAndDepartmentSpecification()
        {
            Include.Add(a => a.Appointments);
            Include.Add(d => d.Department);
        }
    }
}
