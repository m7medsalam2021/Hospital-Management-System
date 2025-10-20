using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.IServices
{
    public interface IMedicalRecordService
    {
        Task<MedicalRecord> CreateMedicalRecordsAsync(MedicalRecordDto medicalRecords);
        Task<MedicalRecord> UpdateMedicalRecordsAsync(int id, MedicalRecordDto medicalRecords);
        Task<MedicalRecord> DeleteMedicalRecordsAsync(int id);
    }
}
