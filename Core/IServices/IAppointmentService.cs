using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.IServices
{
    public interface IAppointmentService
    {
        Task<Appointment> CreateAppointmentAsync(AppointmentDto appointments);
        Task<Appointment> UpdateAppointmentAsync(int id,AppointmentDto appointments);
        Task<Appointment> DeleteAppointmentAsync(int id);
        Task<Appointment> GetAppointmentsByDoctorAndTimeAsync(int doctorId, DateTime appointmentDate);
        Task<Appointment> GetAppointmentsByPatientAndTimeAsync(int patientId, DateTime appointmentDate);
    }
}
