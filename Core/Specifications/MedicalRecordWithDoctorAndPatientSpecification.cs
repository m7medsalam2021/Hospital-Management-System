using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Specifications
{
    public class MedicalRecordWithDoctorAndPatientSpecification : BaseSpecification<MedicalRecord>
    {
        public MedicalRecordWithDoctorAndPatientSpecification(MedicalRecordSpecparams medicalRecordSpecparams) :
            base(P =>
                (!medicalRecordSpecparams.PatientId.HasValue || P.PatientId == medicalRecordSpecparams.PatientId &&
                !medicalRecordSpecparams.DoctorId.HasValue || P.DoctorId == medicalRecordSpecparams.DoctorId))
        {
            Include.Add(d => d.Doctor);
            Include.Add(d => d.Patient);
            ApplyPagination(medicalRecordSpecparams.PageZize * (medicalRecordSpecparams.PageIndex - 1), medicalRecordSpecparams.PageZize);

        }
        public MedicalRecordWithDoctorAndPatientSpecification(int id) : base(i => i.Id == id)
        {
            Include.Add(d => d.Doctor);
            Include.Add(d => d.Patient);
        }
    }
}
