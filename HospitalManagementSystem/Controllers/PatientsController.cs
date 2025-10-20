using AutoMapper;
using Hospital.Core.Entities;
using Hospital.Core.IRepositories;
using Hospital.Core.IServices;
using Hospital.Core.Specifications;
using HospitalManagementSystem.Dtos;
using HospitalManagementSystem.Errors;
using HospitalManagementSystem.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class PatientsController : ApiBaseController
    {
        private readonly IGenericRepo<Patient> _genericRepo;
        private readonly IPatientService _patient;
        private readonly IMapper _mapper;

        public PatientsController(IGenericRepo<Patient> genericRepo,
            IPatientService patient, IMapper mapper)
        {
            _genericRepo = genericRepo;
            _patient = patient;
            _mapper = mapper;
        }
        
        [ProducesResponseType(typeof(PatientsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [CahedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<PatientsDto>>> GetPatients([FromQuery] PatientSpecparams patientSpecparams)
        {
            var spec = new PatientWithAppointmentAndMedicalRecordsSpecification(patientSpecparams);
            var patients = await _genericRepo.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Patient>, IReadOnlyList<PatientsDto>>(patients);
            var countSpec = new PatientWithFiltrationForCountSpecification(patientSpecparams);
            var count = await _genericRepo.GetCountWithSpecAsync(countSpec);
            return Ok(new Pagination<PatientsDto>(patientSpecparams.PageIndex,patientSpecparams.PageZize, count, data));
        }
        
        [ProducesResponseType(typeof(PatientsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [CahedAttribute(600)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientsDto>> GetPatientById(int id)
        {
            var spec = new PatientWithAppointmentAndMedicalRecordsSpecification(id);
            var patients = await _genericRepo.GetByIdWithSpecAsync(spec);
            return Ok(_mapper.Map<Patient, PatientsDto>(patients));
        }

        [Authorize(Roles = "Receptionist")]
        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient([FromBody] PatientsDto patientDto)
        {
            var patient = await _patient.CreatePatientAsync(patientDto);
            return Ok(patient);
        }

        [Authorize(Roles = "Receptionist")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Patient>> UpdatePatient(int id, [FromBody] PatientsDto patients)
        {
            var patient = await _patient.UpdatePatientAsync(id, patients);
            return Ok(patient);
        }

        [Authorize(Roles = "Receptionist")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            var patient = await _patient.DeletePatientAsync(id);
            if (patient is null)
                return NotFound();
            return Ok(patient);
        }

    } 
}
