using Hospital.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hospital.Repository.Data
{
    public static class HospitalContextSeeding
    {
        public static async Task SeedAsync(HospitalContext context)
        {
            // Add Data for Departments
            if (!context.Departments.Any())
            {
                var departmentsData = await File.ReadAllTextAsync("C:\\Users\\Sallam\\Source\\Repos\\HospitalManagementSystem\\Repository\\Data\\DataSeeding\\Department.json");
                var departments = JsonSerializer.Deserialize<List<Department>>(departmentsData);

                if (departments is not null && departments.Count > 0)
                {
                    foreach (var department in departments)
                    {
                        await context.Departments.AddAsync(department);
                    }

                    await context.SaveChangesAsync();
                }
            }


            // Add Data for Doctors
            if (!context.Doctors.Any())
            {
                var doctorsData = await File.ReadAllTextAsync("C:\\Users\\Sallam\\Source\\Repos\\HospitalManagementSystem\\Repository\\Data\\DataSeeding\\Doctor.json");
                var doctors = JsonSerializer.Deserialize<List<Doctor>>(doctorsData);

                if (doctors is not null && doctors.Count > 0)
                {
                    foreach (var doctor in doctors)
                    {
                        await context.Doctors.AddAsync(doctor);
                    }

                    await context.SaveChangesAsync();
                }
            }

            //Add Data for Patients
            if (!context.Patients.Any())
                {
                    var patientsData = await File.ReadAllTextAsync("C:\\Users\\Sallam\\Source\\Repos\\HospitalManagementSystem\\Repository\\Data\\DataSeeding\\Patient.json");
                    var patients = JsonSerializer.Deserialize<List<Patient>>(patientsData);

                    if (patients is not null && patients.Count > 0)
                    {
                        foreach (var patient in patients)
                        {
                            await context.Patients.AddAsync(patient);
                        }

                        await context.SaveChangesAsync();
                    }
                }


            // Add Data for Appointments
            if (!context.Appointments.Any())
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };

                var medicalRecordsData = await File.ReadAllTextAsync("C:\\Users\\Sallam\\Source\\Repos\\HospitalManagementSystem\\Repository\\Data\\DataSeeding\\Appointments.json");
                var medicalRecords = JsonSerializer.Deserialize<List<Appointment>>(medicalRecordsData, options);

                if (medicalRecords is not null && medicalRecords.Count > 0)
                {
                    foreach (var record in medicalRecords)
                    {
                        await context.Appointments.AddAsync(record);
                    }

                    await context.SaveChangesAsync();
                }
            }

            // Add Data for MedicalRecords
            if (!context.MedicalRecords.Any())
            {
                var medicalRecordsData = await File.ReadAllTextAsync("C:\\Users\\Sallam\\Source\\Repos\\HospitalManagementSystem\\Repository\\Data\\DataSeeding\\MedicalRecord.json");
                var medicalRecords = JsonSerializer.Deserialize<List<MedicalRecord>>(medicalRecordsData);

                if (medicalRecords is not null && medicalRecords.Count > 0)
                {
                    foreach (var medicalRecord in medicalRecords)
                    {
                        await context.MedicalRecords.AddAsync(medicalRecord);
                    }

                    await context.SaveChangesAsync();
                }
            }

            // Add Data for Madication
            if (!context.Medications.Any())
            {
                var medicationsData = await File.ReadAllTextAsync("C:\\Users\\Sallam\\Source\\Repos\\HospitalManagementSystem\\Repository\\Data\\DataSeeding\\Medication.json");
                var medications = JsonSerializer.Deserialize<List<Medication>>(medicationsData);

                if (medications is not null && medications.Count > 0)
                {
                    foreach (var medication in medications)
                    {
                        await context.Medications.AddAsync(medication);
                    }

                    await context.SaveChangesAsync();
                }
            }

            // Add Data for PrescriptionDetail
            if (!context.PrescriptionDetails.Any())
            {
                var prescriptionDetailsData = await File.ReadAllTextAsync("C:\\Users\\Sallam\\Source\\Repos\\HospitalManagementSystem\\Repository\\Data\\DataSeeding\\PrescriptionDetail.json");
                var prescriptionDetails = JsonSerializer.Deserialize<List<PrescriptionDetail>>(prescriptionDetailsData);

                if (prescriptionDetails is not null && prescriptionDetails.Count > 0)
                {
                    foreach (var prescriptionDetail in prescriptionDetails)
                    {
                        await context.PrescriptionDetails.AddAsync(prescriptionDetail);
                    }

                    await context.SaveChangesAsync();
                }
            }
        }

    }
}
