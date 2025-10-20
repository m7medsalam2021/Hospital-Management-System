using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Dtos
{
    public class PrescriptionDetailDto
    {
        public int Id { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }

        public int MedicationId { get; set; }
        public string? Medication { get; set; }

        public int MedicalRecordId { get; set; }
        public string? MedicalRecord { get; set; }
    }
}
