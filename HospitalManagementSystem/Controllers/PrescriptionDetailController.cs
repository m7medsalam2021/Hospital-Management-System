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
    public class PrescriptionDetailController : ApiBaseController
    {
        private readonly IGenericRepo<PrescriptionDetail> _genericRepo;
        private readonly IPrescriptionDetailService _prescriptionDetails;
        private readonly IMapper _mapper;

        public PrescriptionDetailController(IGenericRepo<PrescriptionDetail> genericRepo,
            IPrescriptionDetailService prescriptionDetails,  IMapper mapper)
        {
            _genericRepo = genericRepo;
            _prescriptionDetails = prescriptionDetails;
            _mapper = mapper;
        }
        
        [ProducesResponseType(typeof(PrescriptionDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [CahedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PrescriptionDetailDto>>> GetPrescriptionDetail([FromQuery] PrescriptionDetailSpecparams prescriptionDetailSpecparams)
        {
            var spec = new PrescriptionDetailWithMedicationAndMedicalRecordSpecification(prescriptionDetailSpecparams);
            var prescriptionDetails = await _genericRepo.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<PrescriptionDetail>, IReadOnlyList<PrescriptionDetailDto>>(prescriptionDetails);
            var countSpec = new PrescriptionDetailWithFiltrationForCountSpecification(prescriptionDetailSpecparams);
            var count = await _genericRepo.GetCountWithSpecAsync(countSpec);
            return Ok(new Pagination<PrescriptionDetailDto>(prescriptionDetailSpecparams.PageIndex, prescriptionDetailSpecparams.PageZize, count, data));
        }
       
        [ProducesResponseType(typeof(PrescriptionDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [CahedAttribute(600)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PrescriptionDetailDto>> GetPrescriptionDetailtById(int id)
        {
            var spec = new PrescriptionDetailWithMedicationAndMedicalRecordSpecification(id);
            var prescriptionDetail = await _genericRepo.GetByIdWithSpecAsync(spec);
            return Ok(_mapper.Map<PrescriptionDetail, PrescriptionDetailDto>(prescriptionDetail));
        }
        
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public async Task<ActionResult<PrescriptionDetail>> CreatePrescriptionDetail([FromBody] PrescriptionDetailDto prescriptionDetails)
        {
            var prescriptionDetail = await _prescriptionDetails.CreatePrescriptionDetailAsync(prescriptionDetails);
            return Ok(prescriptionDetail);
        }
       
        [Authorize(Roles = "Doctor")]
        [HttpPut("{id}")]
        public async Task<ActionResult<PrescriptionDetail>> UpdatePrescriptionDetail(int id, [FromBody] PrescriptionDetailDto prescriptionDetails)
        {
            var prescriptionDetail = await _prescriptionDetails.UpdatePrescriptionDetailAsync(id, prescriptionDetails);
            return Ok(prescriptionDetail);
        }
       
        [Authorize(Roles = "Doctor")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PrescriptionDetail>> DeletePrescriptionDetail(int id)
        {
            var prescriptionDetail = await _prescriptionDetails.DeletePrescriptionDetailAsync(id);
            if (prescriptionDetail is null)
                return NotFound();
            return Ok(prescriptionDetail);
        }
    }
}
