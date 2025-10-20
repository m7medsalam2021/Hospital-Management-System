using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IServices;
using Hospital.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HospitalContext _hospitalContext;

        public AppointmentService(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }
        public async Task<Appointment> CreateAppointmentAsync(AppointmentDto appointments)
        {
            var appointment = new Appointment
            {
                AppointmentDate = appointments.AppointmentDate,
                Status = appointments.Status,
                DoctorId = appointments.DoctorId,
                PatientId = appointments.PatientId

            };
            _hospitalContext.Add(appointment);
            await _hospitalContext.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment> DeleteAppointmentAsync(int id)
        {
            var appointment = await _hospitalContext.Appointments.FindAsync(id);
            if (appointment == null)
                return null;

            _hospitalContext.Appointments.Remove(appointment);
            await _hospitalContext.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment> UpdateAppointmentAsync(int id, AppointmentDto appointments)
        {
            var appointment = await _hospitalContext.Appointments.FindAsync(id);
            if (appointment == null)
                return null;

            appointment.AppointmentDate = appointments.AppointmentDate;
            appointment.Status = appointments.Status;
            appointment.DoctorId = appointments.DoctorId;
            appointments.PatientId = appointments.PatientId;

            await _hospitalContext.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment> GetAppointmentsByDoctorAndTimeAsync(int doctorId, DateTime appointmentDate)
        {
            return await _hospitalContext.Appointments
                .Where(a => a.DoctorId == doctorId
                            && a.AppointmentDate == appointmentDate)
                .FirstOrDefaultAsync();
        }

        public async Task<Appointment> GetAppointmentsByPatientAndTimeAsync(int patientId, DateTime appointmentDate)
        {
            return await _hospitalContext.Appointments
                  .Where(a => a.PatientId == patientId
                              && a.AppointmentDate == appointmentDate)
                                      .FirstOrDefaultAsync();
        }

    }
}
