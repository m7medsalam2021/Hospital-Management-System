using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Specifications
{
    public class PatientWithFiltrationForCountSpecification : BaseSpecification<Patient>
    {
        public PatientWithFiltrationForCountSpecification(PatientSpecparams patientSpecparams) : 
            base(P => 
                string.IsNullOrEmpty(patientSpecparams.SearchVal)
                || P.Name.ToLower().Contains(patientSpecparams.SearchVal) &&
                (!patientSpecparams.AppointmentId.HasValue 
                || P.AppointmentsId == patientSpecparams.AppointmentId &&
                !patientSpecparams.MedicalRecordId.HasValue 
                || P.MedicalRecordsId == patientSpecparams.MedicalRecordId))
        {
            
        }
    }
}
