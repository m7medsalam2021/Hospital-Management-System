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
    public class MedicationController : ApiBaseController
    {
        private readonly IGenericRepo<Medication> _genericRepo;
        private readonly IMapper _mapper;
        private readonly IMedicationService _medication;

        public MedicationController(IGenericRepo<Medication> genericRepo, 
            IMapper mapper, IMedicationService medication)
        {
            _genericRepo = genericRepo;
            _mapper = mapper;
            _medication = medication;
        }
        
        [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [CahedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Medication>>> GetMedication(string sort)
        {
            //var medication = await _genericRepo.GetAllAsync();
            //return Ok(medication);
            var spec = new MedicationWithprescriptionsDetailsSpecification(sort);
            var medications = await _genericRepo.GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Medication>, IReadOnlyList<MedicationDto>>(medications));
        }
        
        [ProducesResponseType(typeof(MedicationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [CahedAttribute(600)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<Medication>>> GetMedicationById(int id)
        {
            //var medication = await _genericRepo.GetByIdAsync(id);
            //return Ok(medication);
            var spec = new MedicationWithprescriptionsDetailsSpecification(id);
            var medication = await _genericRepo.GetByIdWithSpecAsync(spec);
            return Ok(_mapper.Map<Medication, MedicationDto>(medication));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Medication>> CreateMedication([FromBody] MedicationDto medications)
        {
            var medication = await _medication.CreateMedicationAsync(medications);
            return Ok(medication);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Medication>> UpdateMedication(int id, [FromBody] MedicationDto medications)
        {
            var medication = await _medication.UpdateMedicationAsync(id, medications);
            return Ok(medication);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Medication>> DeleteMedication(int id)
        {
            var medication = await _medication.DeleteMedicationAsync(id);
            if (medication is null)
                return NotFound();
            return Ok(medication);
        }
    }
}
