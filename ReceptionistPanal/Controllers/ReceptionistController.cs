using Hospital.Core.Dtos;
using Hospital.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ReceptionistPanal.Controllers
{
    public class ReceptionistController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ReceptionistController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #region Login
        // Get: Admin/login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser? user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
            {
                ModelState.AddModelError("Email", "Email is Invalid");
                return View();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded || !await _userManager.IsInRoleAsync(user, "Receptionist"))
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }
        #endregion


        #region LogOut
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
        #endregion
    }
}
