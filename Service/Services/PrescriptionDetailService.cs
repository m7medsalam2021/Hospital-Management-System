using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IServices;
using Hospital.Repository.Data;
using HospitalManagementSystem.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Services
{
    public class PrescriptionDetailService : IPrescriptionDetailService
    {
        private readonly HospitalContext _hospitalContext;

        public PrescriptionDetailService(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }
        public async Task<PrescriptionDetail> CreatePrescriptionDetailAsync(PrescriptionDetailDto prescriptionDetails)
        {
            var prescriptionDetail = new PrescriptionDetail
            {
                Dosage = prescriptionDetails.Dosage,
                Frequency = prescriptionDetails.Frequency,
                MedicationId = prescriptionDetails.MedicationId,
                MedicalRecordId = prescriptionDetails.MedicalRecordId
            };
            _hospitalContext.Add(prescriptionDetail);
            await _hospitalContext.SaveChangesAsync();
            return prescriptionDetail;
        }
        public async Task<PrescriptionDetail> UpdatePrescriptionDetailAsync(int id, PrescriptionDetailDto prescriptionDetails)
        {
            var prescriptionDetail = await _hospitalContext.PrescriptionDetails.FindAsync(id);
            if (prescriptionDetail == null)
                return null;
            prescriptionDetail.Dosage = prescriptionDetails.Dosage;
            prescriptionDetail.Frequency = prescriptionDetails.Frequency;
            prescriptionDetail.MedicalRecordId = prescriptionDetails.MedicalRecordId;
            prescriptionDetail.MedicationId = prescriptionDetails.MedicationId;

            await _hospitalContext.SaveChangesAsync();

            return prescriptionDetail;
        }
        public async Task<PrescriptionDetail> DeletePrescriptionDetailAsync(int id)
        {
            var prescriptionDetail = await _hospitalContext.PrescriptionDetails.FindAsync(id);
            if (prescriptionDetail == null)
                return null;

            _hospitalContext.PrescriptionDetails.Remove(prescriptionDetail);
            await _hospitalContext.SaveChangesAsync();
            return prescriptionDetail;
        }
    }
}
