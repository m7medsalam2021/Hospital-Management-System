using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Specifications
{
    public class PatientWithAppointmentAndMedicalRecordsSpecification : BaseSpecification<Patient>
    {
        // This constructor is used for GetAll Patients 
        public PatientWithAppointmentAndMedicalRecordsSpecification(PatientSpecparams patientSpecparams) :
            base(P => string.IsNullOrEmpty(patientSpecparams.SearchVal) 
                || P.Name.ToLower().Contains(patientSpecparams.SearchVal) &&
                (!patientSpecparams.AppointmentId.HasValue 
                || P.AppointmentsId == patientSpecparams.AppointmentId &&
                !patientSpecparams.MedicalRecordId.HasValue 
                || P.MedicalRecordsId == patientSpecparams.MedicalRecordId))
        {
            Include.Add(a => a.Appointments);
            Include.Add(mr => mr.MedicalRecords);

            if (!string.IsNullOrEmpty(patientSpecparams.Sort))
            {
                switch (patientSpecparams.Sort)
                {
                    case "NamePatientAsc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "NamePatientDesc":
                        AddOrderByDesc(p => p.Name);
                        break;
                    default:
                        AddOrderBy(p => p.Id);
                        break;
                }
            }

            ApplyPagination(patientSpecparams.PageZize * (patientSpecparams.PageIndex - 1), patientSpecparams.PageZize);

        }

        public PatientWithAppointmentAndMedicalRecordsSpecification(int id) : base(a => a.Id == id)
        {
            Include.Add(a => a.Appointments);
            Include.Add(mr => mr.MedicalRecords);

        }
    }
}
