using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Specifications
{
    public class MedicalRecordWithFiltrationForCountSpecification : BaseSpecification<MedicalRecord>
    {
        public MedicalRecordWithFiltrationForCountSpecification(MedicalRecordSpecparams medicalRecordSpecparams) :
            base(P =>
                (!medicalRecordSpecparams.PatientId.HasValue || P.PatientId == medicalRecordSpecparams.PatientId &&
                !medicalRecordSpecparams.DoctorId.HasValue || P.DoctorId == medicalRecordSpecparams.DoctorId))
        {
            
        }
    }
}
