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
    public class DoctorService : IDoctorService
    {
        private readonly HospitalContext _hospitalContext;

        public DoctorService(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }
        public async Task<Doctor> CreateDoctorAsync(DoctorsDto doctors)
        {
            var Doctor = new Doctor
            {
                Name = doctors.Name,
                Email = doctors.Email,
                Phone = doctors.Phone,
                Specialty = doctors.Specialty,
                DepartmentId = doctors.DepartmentId
            };
            _hospitalContext.Add(Doctor);
            await _hospitalContext.SaveChangesAsync();
            return Doctor;
        }

        public async Task<Doctor> DeleteDoctorAsync(int id)
        {
            var doctor = await _hospitalContext.Doctors.FindAsync(id);
            if (doctor == null)
                return null;

            _hospitalContext.Doctors.Remove(doctor);
            await _hospitalContext.SaveChangesAsync();
            return doctor;
        }

        public async Task<Doctor> UpdateDoctorAsync(int id, DoctorsDto doctors)
        {
            var doctor = await _hospitalContext.Doctors.FindAsync(id);
            if (doctor == null)
                return null;
            doctor.Name = doctors.Name;
            doctor.Email = doctors.Email;
            doctor.Phone = doctors.Phone;
            doctor.Specialty = doctors.Specialty;
            doctor.DepartmentId = doctors.DepartmentId;

            await _hospitalContext.SaveChangesAsync();
            return doctor;
        }
    }
}
