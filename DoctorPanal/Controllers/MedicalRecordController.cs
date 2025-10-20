using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IRepositories;
using Hospital.Core.IServices;
using Hospital.Core.Specifications;
using HospitalManagementSystem.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DoctorPanal.Controllers
{
    public class MedicalRecordController : Controller
    {
        private readonly IGenericRepo<MedicalRecord> _genericRepo;
        private readonly IMedicalRecordService _medicalRecord;

        public MedicalRecordController(IGenericRepo<MedicalRecord> genericRepo,
            IMedicalRecordService medicalRecord)
        {
            _genericRepo = genericRepo;
            _medicalRecord = medicalRecord;
        }

        #region Index
        public async Task<IActionResult> Index(MedicalRecordSpecparams medicalRecordSpecparams)
        {
            IReadOnlyList<MedicalRecord> medicalRecords = await _genericRepo.GetAllAsync();

            if (medicalRecords is null || medicalRecords.Count == 0)
                return NotFound(new ApiResponse(400));

            return View(medicalRecords);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id, string viewName = nameof(Details))
        {
            if (id == null)
                return BadRequest(new ApiResponse(400));

            MedicalRecord medicalRecord = await _genericRepo.GetByIdAsync(id);

            if (medicalRecord is null)
                return NotFound(new ApiResponse(404));

            return View(viewName, medicalRecord);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicalRecordDto model)
        {
            if (ModelState.IsValid)
            {
                //await _genericRepo.AddAsync(model);
                await _medicalRecord.CreateMedicalRecordsAsync(model);
                return RedirectToAction(nameof(Index));
            }
            else
                ModelState.AddModelError("Name", "MedicalRecord prescription is required");

            return View(model);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int id)
        {
            return await Details(id, nameof(Update));
        }

        [HttpPost]
        public async Task<IActionResult> Update(MedicalRecordDto model)
        {
            if (ModelState.IsValid)
            {
                MedicalRecord existingMedicalRecord = await _genericRepo.GetByIdAsync(model.Id);
                if (existingMedicalRecord is null)
                    return NotFound(new ApiResponse(404));

                await _medicalRecord.UpdateMedicalRecordsAsync(model.Id, model);
                return RedirectToAction(nameof(Index));
            }
            else
                ModelState.AddModelError("Name", "MedicalRecord Prescription is required");

            return View(model);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, nameof(Delete));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MedicalRecordDto model)
        {
            try
            {
                MedicalRecord existingMedicalRecord = await _genericRepo.GetByIdAsync(model.Id);
                if (existingMedicalRecord is null)
                    return NotFound(new ApiResponse(404));

                await _medicalRecord.DeleteMedicalRecordsAsync(model.Id);
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
