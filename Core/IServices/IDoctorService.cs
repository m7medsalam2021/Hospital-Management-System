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
    public interface IDoctorService
    {
        Task<Doctor> CreateDoctorAsync(DoctorsDto doctors);
        Task<Doctor> UpdateDoctorAsync(int id, DoctorsDto doctors);
        Task<Doctor> DeleteDoctorAsync(int id);
    }
}
