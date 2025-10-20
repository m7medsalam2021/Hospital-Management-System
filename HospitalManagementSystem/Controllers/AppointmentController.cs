using AutoMapper;
using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IRepositories;
using Hospital.Core.IServices;
using Hospital.Core.Specifications;
using HospitalManagementSystem.Errors;
using HospitalManagementSystem.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class AppointmentController : ApiBaseController
    {
        private readonly IGenericRepo<Appointment> _genericRepo;
        private readonly IMapper _mapper;
        private readonly IAppointmentService _appointment;

        public AppointmentController(IGenericRepo<Appointment> genericRepo,
            IMapper mapper, IAppointmentService appointment)
        {
            _genericRepo = genericRepo;
            _mapper = mapper;
            _appointment = appointment;
        }
        [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [CahedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAllAppointment()
        {
            //var appointment = await _genericRepo.GetAllAsync();
            //return Ok(appointment);
            var spec = new AppiontmentWithDoctorAndPatientSpecification();
            var appointment = await _genericRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Appointment>, IReadOnlyList<AppointmentDto>>(appointment));
        }
        [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [CahedAttribute(600)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<Appointment>>> GetAllAppointmentById(int id)
        {
            //var appointment = await _genericRepo.GetByIdAsync(id);
            //return Ok(appointment);
            var spec = new AppiontmentWithDoctorAndPatientSpecification(id);
            var appointment = await _genericRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Appointment>, IReadOnlyList<AppointmentDto>>(appointment));

        }
        [Authorize(Roles = "Receptionist")]
        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            var conflictingAppointment = await _appointment.GetAppointmentsByDoctorAndTimeAsync(
                                           appointmentDto.DoctorId, appointmentDto.AppointmentDate);

            if (conflictingAppointment != null)
            {
                return BadRequest("This doctor already has an appointment at the selected time.");
            }

            var patientConflict = await _appointment.GetAppointmentsByPatientAndTimeAsync(
                appointmentDto.PatientId,
                appointmentDto.AppointmentDate
            );

            if (patientConflict != null)
            {
                return BadRequest("This patient already has an appointment at the selected time.");
            }

            var appointment = await _appointment.CreateAppointmentAsync(appointmentDto);
            return Ok(appointment);
            
        }
        [Authorize(Roles = "Receptionist")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Appointment>> UpdateDoctor(int id, [FromBody] AppointmentDto appointmentDto)
        {
            var appointment = await _appointment.UpdateAppointmentAsync(id, appointmentDto);
            return Ok(appointment);
        }
        [Authorize(Roles = "Receptionist")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Appointment>> DeleteDoctor(int id)
        {
            var appointment = await _appointment.DeleteAppointmentAsync(id);
            if (appointment is null)
                return NotFound();
            return Ok(appointment);
        }

    }
}
