using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using HospitalManagementSystem.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.IServices
{
    public interface IMedicationService
    {
        Task<Medication> CreateMedicationAsync(MedicationDto medications);
        Task<Medication> UpdateMedicationAsync(int id, MedicationDto medications);
        Task<Medication> DeleteMedicationAsync(int id);
    }
}
