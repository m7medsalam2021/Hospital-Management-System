using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IRepositories;
using Hospital.Core.IServices;
using HospitalManagementSystem.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanal.Controllers
{
    public class MedicationController : Controller
    {
        private readonly IGenericRepo<Medication> _genericRepo;
        private readonly IMedicationService _medication;

        public MedicationController(IGenericRepo<Medication> genericRepo, IMedicationService medication)
        {
            _genericRepo = genericRepo;
            _medication = medication;
        }
        
        #region Index
        public async Task<IActionResult> Index()
        {
            IReadOnlyList<Medication> medications = await _genericRepo.GetAllAsync();

            if (medications is null || medications.Count == 0)
                return NotFound(new ApiResponse(400));

            return View(medications);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id, string viewName = nameof(Details))
        {
            if (id == null)
                return BadRequest(new ApiResponse(400));

            Medication medication = await _genericRepo.GetByIdAsync(id);

            if (medication is null)
                return NotFound(new ApiResponse(404));

            return View(viewName, medication);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicationDto model)
        {
            if (ModelState.IsValid)
            {
                //await _genericRepo.AddAsync(model);
                await _medication.CreateMedicationAsync(model);
                return RedirectToAction(nameof(Index));
            }
            else
                ModelState.AddModelError("Name", "Medication name is required");

            return View(model);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int id)
        {
            return await Details(id, nameof(Update));
        }

        [HttpPost]
        public async Task<IActionResult> Update(MedicationDto model)
        {
            if (ModelState.IsValid)
            {
                Medication existingMedication = await _genericRepo.GetByIdAsync(model.Id);
                if (existingMedication is null)
                    return NotFound(new ApiResponse(404));

                await _medication.UpdateMedicationAsync(model.Id, model);
                return RedirectToAction(nameof(Index));
            }
            else
                ModelState.AddModelError("Name", "Medication Name is required");

            return View(model);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, nameof(Delete));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(MedicationDto model)
        {
            try
            {
                Medication existingMedication = await _genericRepo.GetByIdAsync(model.Id);
                if (existingMedication is null)
                    return NotFound(new ApiResponse(404));

                await _medication.DeleteMedicationAsync(model.Id);
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
