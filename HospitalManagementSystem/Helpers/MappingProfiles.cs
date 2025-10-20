using AutoMapper;
using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using HospitalManagementSystem.Dtos;

namespace HospitalManagementSystem.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Patient, PatientsDto>()
                .ForMember(a => a.Appointments,
                        o => o.MapFrom(s => s.Appointments.FirstOrDefault() != null
                        ? $"{s.Appointments.FirstOrDefault().Status}" +
                        $" on {s.Appointments.FirstOrDefault().AppointmentDate:yyyy-MM-dd}" : null))
                .ForMember(mr => mr.MedicalRecords,
                        o => o.MapFrom(s => s.MedicalRecords.FirstOrDefault() != null
                        ? $"Diagnosis is: {s.MedicalRecords.FirstOrDefault().Diagnosis}" +
                        $" And Prescription is: {s.MedicalRecords.FirstOrDefault().Prescription}" : null));




            CreateMap<Doctor, DoctorsDto>()
                .ForMember(a => a.Appointments, o => o.MapFrom(s => s.Appointments.FirstOrDefault() != null
                ? $"{s.Appointments.FirstOrDefault().Status}" +
                $"on {s.Appointments.FirstOrDefault().AppointmentDate:yyyy-MM-dd}" : null))
                .ForMember(d => d.Departments, o => o.MapFrom(s => s.Department.Name));

            CreateMap<Department, DepartmentToReturnDoctorsDto>()
                .ForMember(a => a.Doctors, o => o.MapFrom(s => s.Doctors.FirstOrDefault() != null
                ? $"{s.Doctors.FirstOrDefault().Name}" : null));

            CreateMap<Department, DepartmentDto>();

            CreateMap<MedicalRecord, MedicalRecordDto>()
                .ForMember(p => p.Patient, o => o.MapFrom(s => s.Patient.Name))
                .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Doctor.Name));

            CreateMap<Appointment, AppointmentDto>()
                .ForMember(p => p.Patient, o => o.MapFrom(s => s.Patient.Name))
                .ForMember(d => d.Doctor, o => o.MapFrom(s => s.Doctor.Name));

            CreateMap<Medication, MedicationDto>()
                .ForMember(p => p.PrescriptionDetails, o => o.MapFrom(s => s.PrescriptionDetails.FirstOrDefault() != null
                ? $"Nmae is: {s.Name}" + $" Quantity is: {s.Quantity}" + $" Description is: {s.Description}" : null));

            CreateMap<PrescriptionDetail, PrescriptionDetailDto>()
                .ForMember(p => p.Medication, o => o.MapFrom(s => s.Medication.Name))
                .ForMember(p => p.Medication, o => o.MapFrom(s => s.Medication.Description))
                .ForMember(p => p.Medication, o => o.MapFrom(s => s.Medication.Quantity))
                .ForMember(p => p.MedicalRecord, o => o.MapFrom(s => s.MedicalRecord.Diagnosis))
                .ForMember(p => p.MedicalRecord, o => o.MapFrom(s => s.MedicalRecord.Prescription))
                ;

        }
    }
}
