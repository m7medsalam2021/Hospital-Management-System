using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IRepositories;
using Hospital.Core.IServices;
using HospitalManagementSystem.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanal.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IGenericRepo<Department> _genericRepo;
        private readonly IDepartmentService _department;

        public DepartmentController(IGenericRepo<Department> genericRepo, IDepartmentService department)
        {
            _genericRepo = genericRepo;
            _department = department;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            IReadOnlyList<Department> departments = await _genericRepo.GetAllAsync();

            if (departments is null || departments.Count == 0)
                return NotFound(new ApiResponse(400));

            return View(departments);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id, string viewName = nameof(Details))
        {
            if (id == null)
                return BadRequest(new ApiResponse(400));

            Department department = await _genericRepo.GetByIdAsync(id);

            if (department is null)
                return NotFound(new ApiResponse(404));

            return View(viewName, department);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentToReturnDoctorsDto model)
        {
            if (ModelState.IsValid)
            {
                //await _genericRepo.AddAsync(model);
                await _department.CreateDepartmentAsync(model);
                return RedirectToAction(nameof(Index));
            }
            else
                ModelState.AddModelError("Name", "Department name is required");

            return View(model);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int id)
        {
            return await Details(id, nameof(Update));
        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmentToReturnDoctorsDto model)
        {
            if (ModelState.IsValid)
            {
                Department existingDepartment = await _genericRepo.GetByIdAsync(model.Id);
                if (existingDepartment is null)
                    return NotFound(new ApiResponse(404));

                await _department.UpdateDepartmentAsync(model.Id, model);
                return RedirectToAction(nameof(Index));
            }
            else
                ModelState.AddModelError("Name", "Department Name is required");

            return View(model);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, nameof(Delete));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentToReturnDoctorsDto model)
        {
            try
            {
                Department existingDepartment = await _genericRepo.GetByIdAsync(model.Id);
                if (existingDepartment is null)
                    return NotFound(new ApiResponse(404));

                await _department.DeleteDepartmentAsync(model.Id);
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
