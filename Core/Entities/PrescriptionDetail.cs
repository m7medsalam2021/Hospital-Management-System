using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Entities
{
    public class PrescriptionDetail : BaseEntity
    {
        public string Dosage { get; set; }
        public string Frequency { get; set; }

        public int MedicationId { get; set; }
        public Medication Medication { get; set; }

        public int MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
    }
}
