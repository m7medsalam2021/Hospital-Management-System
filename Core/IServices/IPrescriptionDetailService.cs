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
    public interface IPrescriptionDetailService
    {
        Task<PrescriptionDetail> CreatePrescriptionDetailAsync(PrescriptionDetailDto prescriptionDetails);
        Task<PrescriptionDetail> UpdatePrescriptionDetailAsync(int id, PrescriptionDetailDto prescriptionDetails);
        Task<PrescriptionDetail> DeletePrescriptionDetailAsync(int id);
    }
}
