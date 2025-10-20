using Hospital.Core.Entities;
using HospitalManagementSystem.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.IServices
{
    public interface IPatientService
    {
        Task<Patient> CreatePatientAsync(PatientsDto patients);
        Task<Patient> UpdatePatientAsync(int id, PatientsDto patients);
        Task<Patient> DeletePatientAsync(int id);
    }
}
