using AutoMapper;
using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IRepositories;
using Hospital.Core.IServices;
using Hospital.Core.Specifications;
using Hospital.Service.Services;
using HospitalManagementSystem.Errors;
using HospitalManagementSystem.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class MedicalRecordController : ApiBaseController
    {
        private readonly IGenericRepo<MedicalRecord> _genericRepo;
        private readonly IMedicalRecordService _medicalRecord;
        private readonly IMapper _mapper;

        public MedicalRecordController(IGenericRepo<MedicalRecord> genericRepo, 
            IMedicalRecordService medicalRecord, IMapper mapper)
        {
            _genericRepo = genericRepo;
            _medicalRecord = medicalRecord;
            _mapper = mapper;
        }
        
        [ProducesResponseType(typeof(MedicalRecordDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [CahedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MedicalRecord>>> GetAllMedicalRecords([FromQuery] MedicalRecordSpecparams medicalRecordSpecparams)
        {
            var spec = new MedicalRecordWithDoctorAndPatientSpecification(medicalRecordSpecparams);
            var medicalRecord = await _genericRepo.GetAllWithSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<MedicalRecord>, IReadOnlyList<MedicalRecordDto>>(medicalRecord);
            var countSpec = new MedicalRecordWithFiltrationForCountSpecification(medicalRecordSpecparams);
            var count = await _genericRepo.GetCountWithSpecAsync(countSpec);
            return Ok(new Pagination<MedicalRecordDto>(medicalRecordSpecparams.PageIndex, medicalRecordSpecparams.PageZize, count, data));
        }
        
        [ProducesResponseType(typeof(MedicalRecordDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [CahedAttribute(600)]
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecordsById(int id)
        {
            //var medicalRecords = await _genericRepo.GetByIdAsync(id);
            //return Ok(medicalRecords);
            var spec = new MedicalRecordWithDoctorAndPatientSpecification(id);
            var medicalRecord = await _genericRepo.GetByIdWithSpecAsync(spec);
            return Ok(_mapper.Map<MedicalRecord, MedicalRecordDto>(medicalRecord));
        }
        
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> CreateMedicalRecords([FromBody] MedicalRecordDto medicalRecords)
        {
            var medicalRecord = await _medicalRecord.CreateMedicalRecordsAsync(medicalRecords);
            return Ok(medicalRecord);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPut("{id}")]
        public async Task<ActionResult<MedicalRecord>> UpdateMedicalRecords(int id, [FromBody] MedicalRecordDto medicalRecords)
        {
            var medicalRecord = await _medicalRecord.UpdateMedicalRecordsAsync(id, medicalRecords);
            return Ok(medicalRecord);
        }

        [Authorize(Roles = "Doctor")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicalRecord>> DeleteMedicalRecords(int id)
        {
            var medicalRecord = await _medicalRecord.DeleteMedicalRecordsAsync(id);
            if (medicalRecord is null)
                return NotFound();
            return Ok(medicalRecord);
        }
    }
}
