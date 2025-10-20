using AutoMapper;
using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IRepositories;
using Hospital.Core.IServices;
using Hospital.Core.Specifications;
using HospitalManagementSystem.Dtos;
using HospitalManagementSystem.Errors;
using HospitalManagementSystem.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HospitalManagementSystem.Controllers
{
    public class DoctorsController : ApiBaseController
    {
        private readonly IGenericRepo<Doctor> _genericRepo;
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorsController(IGenericRepo<Doctor> genericRepo, 
            IDoctorService doctorService, IMapper mapper)
        {
            _genericRepo = genericRepo;
            _doctorService = doctorService;
            _mapper = mapper;
        }
       
        [ProducesResponseType(typeof(DoctorsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [CahedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Doctor>>> GetDoctors([FromQuery] DoctorSpecParams doctorSpecParams)
        {
            var spec = new DoctorWithAppointmentAndDepartmentSpecification(doctorSpecParams);
            var doctors = await _genericRepo.GetAllWithSpecAsync(spec);
         
            var data = _mapper.Map<IReadOnlyList<Doctor>, IReadOnlyList<DoctorsDto>>(doctors);
            var countSpec = new DoctorWithFiltrationForCountSpecification(doctorSpecParams);
            var count = await _genericRepo.GetCountWithSpecAsync(countSpec);
            return Ok(new Pagination<DoctorsDto>(doctorSpecParams.PageIndex, doctorSpecParams.PageZize, count, data));
        }
        
        [ProducesResponseType(typeof(DoctorsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)] 
        [CahedAttribute(600)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<DoctorsDto>>> GetDoctorsById(int id)
        {
            var spec = new DoctorWithAppointmentAndDepartmentSpecification(id);
            var doctors = await _genericRepo.GetByIdWithSpecAsync(spec);
            return Ok(_mapper.Map<Doctor,DoctorsDto>(doctors));
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor([FromBody] DoctorsDto doctorDto)
        {
            var doctor = await _doctorService.CreateDoctorAsync(doctorDto);
            return Ok(doctor);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Doctor>> UpdateDoctor(int id, [FromBody] DoctorsDto doctors)
        {
            var doctor = await _doctorService.UpdateDoctorAsync(id, doctors);
            return Ok(doctor);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Doctor>> DeleteDoctor(int id)
        {
            var doctor = await _doctorService.DeleteDoctorAsync(id);
            if (doctor is null)
                return NotFound();
            return Ok(doctor);
        }
    }
}
