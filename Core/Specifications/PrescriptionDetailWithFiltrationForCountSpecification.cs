using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Specifications
{
    public class PrescriptionDetailWithFiltrationForCountSpecification : BaseSpecification<PrescriptionDetail>
    {
        public PrescriptionDetailWithFiltrationForCountSpecification(PrescriptionDetailSpecparams prescriptionDetailSpecparams):
                                 base(P =>
                (!prescriptionDetailSpecparams.MedicationId.HasValue || P.MedicationId == prescriptionDetailSpecparams.MedicationId &&
                !prescriptionDetailSpecparams.MedicalRecordId.HasValue || P.MedicalRecordId == prescriptionDetailSpecparams.MedicalRecordId))

        {

        }
    }
}
