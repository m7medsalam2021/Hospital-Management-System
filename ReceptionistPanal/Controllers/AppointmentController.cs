using Hospital.Core.Dtos;
using Hospital.Core.Entities;
using Hospital.Core.IRepositories;
using Hospital.Core.IServices;
using HospitalManagementSystem.Errors;
using Microsoft.AspNetCore.Mvc;

namespace ReceptionistPanal.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IGenericRepo<Appointment> _genericRepo;
        private readonly IAppointmentService _appointment;

        public AppointmentController(IGenericRepo<Appointment> genericRepo, 
            IAppointmentService appointment)
        {
            _genericRepo = genericRepo;
            _appointment = appointment;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            IReadOnlyList<Appointment> appointments = await _genericRepo.GetAllAsync();

            if (appointments is null || appointments.Count == 0)
                return NotFound(new ApiResponse(400));

            return View(appointments);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id, string viewName = nameof(Details))
        {
            if (id == null)
                return BadRequest(new ApiResponse(400));

            Appointment appointment = await _genericRepo.GetByIdAsync(id);

            if (appointment is null)
                return NotFound(new ApiResponse(404));

            return View(viewName, appointment);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppointmentDto model)
        {
            if (ModelState.IsValid)
            {
                await _appointment.CreateAppointmentAsync(model);
                return RedirectToAction(nameof(Index));
            }
            else
                ModelState.AddModelError("Name", "Appointment Status is required");

            return View(model);
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int id)
        {
            return await Details(id, nameof(Update));
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppointmentDto model)
        {
            if (ModelState.IsValid)
            {
                Appointment existingAppointment = await _genericRepo.GetByIdAsync(model.Id);
                if (existingAppointment is null)
                    return NotFound(new ApiResponse(404));

                await _appointment.UpdateAppointmentAsync(model.Id, model);
                return RedirectToAction(nameof(Index));
            }
            else
                ModelState.AddModelError("Name", "Appointment status is required");

            return View(model);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, nameof(Delete));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AppointmentDto model)
        {
            try
            {
                Appointment existingAppointment = await _genericRepo.GetByIdAsync(model.Id);
                if (existingAppointment is null)
                    return NotFound(new ApiResponse(404));

                await _appointment.DeleteAppointmentAsync(model.Id);
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
