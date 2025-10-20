using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Specifications
{
    public class PrescriptionDetailWithMedicationAndMedicalRecordSpecification : BaseSpecification<PrescriptionDetail>
    {
        public PrescriptionDetailWithMedicationAndMedicalRecordSpecification(PrescriptionDetailSpecparams prescriptionDetailSpecparams) :
            base(P =>
                (!prescriptionDetailSpecparams.MedicationId.HasValue || P.MedicationId == prescriptionDetailSpecparams.MedicationId&&
                !prescriptionDetailSpecparams.MedicalRecordId.HasValue || P.MedicalRecordId == prescriptionDetailSpecparams.MedicalRecordId))
        { 
            Include.Add(m => m.MedicalRecord);
            Include.Add(m => m.Medication);
        }
        public PrescriptionDetailWithMedicationAndMedicalRecordSpecification(int id) : base(i => i.Id == id)
        {
            Include.Add(m => m.MedicalRecord);
            Include.Add(m => m.Medication);
        }
        public PrescriptionDetailWithMedicationAndMedicalRecordSpecification()
        {
            Include.Add(m => m.MedicalRecord);
            Include.Add(m => m.Medication);
        }
    }
}
