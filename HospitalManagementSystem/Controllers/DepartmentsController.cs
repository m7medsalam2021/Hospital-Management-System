using AutoMapper;
using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IRepositories;
using Hospital.Core.IServices;
using Hospital.Core.Specifications;
using HospitalManagementSystem.Errors;
using HospitalManagementSystem.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class DepartmentsController : ApiBaseController
    {
        private readonly IGenericRepo<Department> _genericRepo;
        private readonly IDepartmentService _department;
        private readonly IMapper _mapper;

        public DepartmentsController(IGenericRepo<Department> genericRepo,
            IDepartmentService department, IMapper mapper)
        {
            _genericRepo = genericRepo;
            _department = department;
            _mapper = mapper;
        }
        
        [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [CahedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DepartmentDto>>> GetDepartments(string sort)
        {
            //var department = await _genericRepo.GetAllAsync();
            //return Ok(department);
            var spec = new DepartmentWithDoctorsSpecification(sort);
            var department = await _genericRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Department>, IReadOnlyList<DepartmentDto>>(department));
            
        }
       
        [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [CahedAttribute(600)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<DepartmentToReturnDoctorsDto>>> GetDepartmentsById(int id)
        {
            var spec = new DepartmentWithDoctorsSpecification(id);
            var department = await _genericRepo.GetByIdWithSpecAsync(spec);

            if (department == null) return NotFound();

            var departmentDto = new DepartmentToReturnDoctorsDto
            {
                Name = department.Name,
                Location = department.Location,
                Doctors = department.Doctors.Select(d => new DoctorsDto
                {
                    Name = d.Name,
                    Email = d.Email,
                    Phone = d.Phone,
                    Specialty = d.Specialty,
                    DepartmentId = d.DepartmentId,
                    Departments = d.Department?.Name,
                    AppointmentId = d.AppointmentsId,
                    Appointments = d.Appointments != null
                ? string.Join(", ", d.Appointments.Select(a => $"{a.Status} on {a.AppointmentDate:yyyy-MM-dd}"))
                : ""
                }).ToList()
            };

            return Ok(departmentDto);
        }

        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment([FromBody] DepartmentToReturnDoctorsDto departmentDto)
        {
            var department = await _department.CreateDepartmentAsync(departmentDto);
            return Ok(department);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Department>> UpdateDepartment(int id, [FromBody] DepartmentToReturnDoctorsDto departments)
        {
            var department = await _department.UpdateDepartmentAsync(id, departments);
            return Ok(department);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var department = await _department.DeleteDepartmentAsync(id);
            if (department is null)
                return NotFound();
            return Ok(department);
        }
    }
}
