using AutoMapper;
using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IRepositories;
using Hospital.Core.IServices;
using Hospital.Core.Specifications;
using HospitalManagementSystem.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoctorPanal.Controllers
{
    public class PrescriptionDetailController : Controller
    {
        private readonly IGenericRepo<PrescriptionDetail> _genericRepo;
        private readonly IPrescriptionDetailService _prescriptionDetail;
        private readonly IMapper _mapper;

        public PrescriptionDetailController(IGenericRepo<PrescriptionDetail> genericRepo,
            IPrescriptionDetailService prescriptionDetail, IMapper mapper)
        {
            _genericRepo = genericRepo;
            _prescriptionDetail = prescriptionDetail;
            _mapper = mapper;
        }

        #region Index
        // GET: /PrescriptionDetails
        public async Task<IActionResult> Index()
        {

            PrescriptionDetailWithMedicationAndMedicalRecordSpecification spec = new PrescriptionDetailWithMedicationAndMedicalRecordSpecification();

            IReadOnlyList<PrescriptionDetail> prescriptionDetails = await _genericRepo.GetAllWithSpecAsync(spec);

            IReadOnlyList<PrescriptionDetailDto> mapped = _mapper.Map<IReadOnlyList<PrescriptionDetail>, IReadOnlyList<PrescriptionDetailDto>>(prescriptionDetails);

            return View(mapped);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id, string viewName = nameof(Details))
        {
            if (id == null)
                return BadRequest(new ApiResponse(400));

            PrescriptionDetailWithMedicationAndMedicalRecordSpecification spec = new PrescriptionDetailWithMedicationAndMedicalRecordSpecification(id);

            PrescriptionDetail prescriptionDetail = await _genericRepo.GetByIdWithSpecAsync(spec);

            if (prescriptionDetail is null)
                return NotFound(new ApiResponse(400));

            PrescriptionDetailDto mappedDoctor = _mapper.Map<PrescriptionDetail, PrescriptionDetailDto>(prescriptionDetail);

            return View(viewName, mappedDoctor);
        }
        #endregion

        #region Create
        // GET: /PrescriptionDetail/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: /PrescriptionDetail/Create
        [HttpPost]
        public async Task<IActionResult> Create(PrescriptionDetailDto model)
        {
            if (ModelState.IsValid)
            {
                await _prescriptionDetail.CreatePrescriptionDetailAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        #endregion

        #region Update
        // GET: /PrescriptionDetail/Update
        public async Task<IActionResult> Update(int id)
        { 
            return await Details(id, nameof(Update));
        }

        // POST: /PrescriptionDetail/Update
        [HttpPost]
        public async Task<IActionResult> Update(PrescriptionDetailDto model)
        {
            if (ModelState.IsValid)
            {
                PrescriptionDetail existingPrescriptionDetail = await _genericRepo.GetByIdAsync(model.Id);
                if (existingPrescriptionDetail is null)
                    return NotFound(new ApiResponse(400));


                await _prescriptionDetail.UpdatePrescriptionDetailAsync(model.Id, model);
                return RedirectToAction(nameof(Index));

            }

            return View(model);
        }
        #endregion

        #region Delete
        // GET: /PrescriptionDetail/Delete
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, nameof(Delete));
        }

        // POST: /PrescriptionDetail/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(PrescriptionDetailDto model)
        {
            try
            {
                PrescriptionDetail prescriptionDetail = await _genericRepo.GetByIdAsync(model.Id);
                await _prescriptionDetail.DeletePrescriptionDetailAsync(model.Id);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong, please try again later");
            }
            return View(model);
        }
        #endregion
    }
}
