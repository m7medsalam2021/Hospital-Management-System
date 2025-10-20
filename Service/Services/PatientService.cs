using Hospital.Core.Entities;
using Hospital.Core.IServices;
using Hospital.Repository.Data;
using HospitalManagementSystem.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Services
{
    public class PatientService : IPatientService
    {
        private readonly HospitalContext _hospitalContext;

        public PatientService(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }
        public async Task<Patient> CreatePatientAsync(PatientsDto patients)
        {
            var patient = new Patient
            {
                Name = patients.Name,
                Email = patients.Email, 
                Address = patients.Address,
                Phone = patients.Phone,
                BirthDay = DateTime.Now,
            };
            _hospitalContext.Add(patient);
            await _hospitalContext.SaveChangesAsync();
            return patient;
        }
        public async Task<Patient> UpdatePatientAsync(int id, PatientsDto patients)
        {
            var patient = await _hospitalContext.Patients.FindAsync(id);
            if (patient == null)
                return null;
            patient.Name = patients.Name;
            patient.Email = patients.Email;
            patient.Address = patients.Address;
            patient.Phone = patients.Phone;
            patient.BirthDay = patients.BirthDay;

            await _hospitalContext.SaveChangesAsync();

            return patient;
        }
        public async Task<Patient> DeletePatientAsync(int id)
        {
            var patient = await _hospitalContext.Patients.FindAsync(id);
            if (patient == null)
                return null;

            _hospitalContext.Patients.Remove(patient);
            await _hospitalContext.SaveChangesAsync();
            return patient;
        }

    
    }
}
