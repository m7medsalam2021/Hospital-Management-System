using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IServices;
using Hospital.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly HospitalContext _hospitalContext;

        public MedicalRecordService(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }
        public async Task<MedicalRecord> CreateMedicalRecordsAsync(MedicalRecordDto medicalRecords)
        {
            var medicalRecord = new MedicalRecord
            {
                Diagnosis = medicalRecords.Diagnosis,
                Prescription = medicalRecords.Prescription,
                DoctorId = medicalRecords.DoctorId,
                PatientId = medicalRecords.PatientId
            };
            _hospitalContext.Add(medicalRecord);
            await _hospitalContext.SaveChangesAsync();
            return medicalRecord;

        }

        public async Task<MedicalRecord> DeleteMedicalRecordsAsync(int id)
        {
            var medicalRecord = await _hospitalContext.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
                return null;

            _hospitalContext.MedicalRecords.Remove(medicalRecord);
            await _hospitalContext.SaveChangesAsync();
            return medicalRecord;
        }

        public async Task<MedicalRecord> UpdateMedicalRecordsAsync(int id, MedicalRecordDto medicalRecords)
        {
            var medicalRecord = await _hospitalContext.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
                return null;

            _hospitalContext.MedicalRecords.Remove(medicalRecord);
            await _hospitalContext.SaveChangesAsync();
            return medicalRecord;
        }
    }
}
