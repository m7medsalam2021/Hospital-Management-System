using AdminPanal.Models;
using AutoMapper;
using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IRepositories;
using Hospital.Core.IServices;
using Hospital.Core.Specifications;
using HospitalManagementSystem.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanal.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IGenericRepo<Doctor> _genericRepo;
        private readonly IGenericRepo<Department> _departmentRepo;
        private readonly IDoctorService _doctor;
        private readonly IMapper _mapper;

        public DoctorController(IGenericRepo<Doctor> genericRepo,
            IGenericRepo<Department> departmentRepo,
            IDoctorService doctor, IMapper mapper)
        {
            _genericRepo = genericRepo;
            _departmentRepo = departmentRepo;
            _doctor = doctor;
            _mapper = mapper;
        }

        #region Index
        // GET: /Doctor
        public async Task<IActionResult> Index()
        {

            DoctorWithAppointmentAndDepartmentSpecification spec = new DoctorWithAppointmentAndDepartmentSpecification();

            IReadOnlyList<Doctor> doctors = await _genericRepo.GetAllWithSpecAsync(spec);

            IReadOnlyList<DoctorsDto> mapped = _mapper.Map<IReadOnlyList<Doctor>, IReadOnlyList<DoctorsDto>>(doctors);

            return View(mapped);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id, string viewName = nameof(Details))
        {
            if (id == null)
                return BadRequest(new ApiResponse(400));

            DoctorWithAppointmentAndDepartmentSpecification spec = new DoctorWithAppointmentAndDepartmentSpecification(id);

            Doctor doctor = await _genericRepo.GetByIdWithSpecAsync(spec);

            if (doctor is null)
                return NotFound(new ApiResponse(400));

            DoctorsDto mappedDoctor = _mapper.Map<Doctor, DoctorsDto>(doctor);

            return View(viewName, mappedDoctor);
        }
        #endregion

        #region Create
        // GET: /Doctor/Create
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentRepo.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View();
        }

        // POST: /Doctor/Create
        [HttpPost]
        public async Task<IActionResult> Create(DoctorsDto model)
        {
            if (ModelState.IsValid)
            {
                await _doctor.CreateDoctorAsync(model);
                return RedirectToAction(nameof(Index));
            }
            var departments = await _genericRepo.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View(model);
        }
        #endregion

        #region Update
        // GET: /Doctor/Update
        public async Task<IActionResult> Update(int id)
        {
            var departments = await _departmentRepo.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return await Details(id, nameof(Update));
        }

        // POST: /Doctor/Update
        [HttpPost]
        public async Task<IActionResult> Update(DoctorsDto model)
        {
            if (ModelState.IsValid)
            {
                Doctor existingDoctor = await _genericRepo.GetByIdAsync(model.Id);
                if (existingDoctor is null)
                    return NotFound(new ApiResponse(400));


                await _doctor.UpdateDoctorAsync(model.Id, model);
                return RedirectToAction(nameof(Index));

            }

            return View(model);
        }
        #endregion

        #region Delete
        // GET: /Product/Delete
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, nameof(Delete));
        }

        // POST: /Product/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(DoctorsDto model)
        {
            try
            {
                Doctor product = await _genericRepo.GetByIdAsync(model.Id);


                //_unitOfWork.Repository<Product>().Delete(product);

                await _doctor.DeleteDoctorAsync(model.Id);
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
