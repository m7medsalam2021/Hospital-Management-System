using Hospital.Core.Dtos;
using Hospital.Core.Entities.Identity;
using Hospital.Core.IServices;
using HospitalManagementSystem.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _token;

        public AccountsController(UserManager<AppUser> userManager, 
                                  SignInManager<AppUser> signInManager,
                                  ITokenService token)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            //email checking 
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            //password checking
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            //valid --> return user Dto
            return Ok(new UserDto()
            {
                DisplayName = user.Name,
                Email = user.Email,
                Token = await _token.CreateTokenAsync(user, _userManager)
            });

        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            var user = new AppUser()
            {
                Email = register.Email,
                Name = register.DisplayName,
                PhoneNumber = register.PhoneNumber,
                UserName = register.Email.Split("@")[0]
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));


            return Ok(new UserDto()
            {
                DisplayName = user.Name,
                Email = user.Email,
                Token = await _token.CreateTokenAsync(user, _userManager)
            });
        }
    }
}
