using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IServices;
using Hospital.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly HospitalContext _hospitalContext;

        public MedicationService(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }
        public async Task<Medication> CreateMedicationAsync(MedicationDto medications)
        {
            var medication = new Medication
            {
                Name = medications.Name,
                Description = medications.Description,
                Quantity = medications.Quantity
            };

            _hospitalContext.Add(medication);
            await _hospitalContext.SaveChangesAsync();
            return medication;
        }

        public async Task<Medication> DeleteMedicationAsync(int id)
        {
            var medication = await _hospitalContext.Medications.FindAsync(id);
            if (medication == null)
                return null;

            _hospitalContext.Medications.Remove(medication);
            await _hospitalContext.SaveChangesAsync();
            return medication;
        }

        public async Task<Medication> UpdateMedicationAsync(int id, MedicationDto medications)
        {
            var medication = await _hospitalContext.Medications.FindAsync(id);
            if (medication == null)
                return null;

            medication.Name = medications.Name;
            medication.Description = medications.Description;
            medication.Quantity = medications.Quantity;
            
            await _hospitalContext.SaveChangesAsync();
            return medication;
        }
    }
}
