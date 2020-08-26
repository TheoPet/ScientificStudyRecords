using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScientificStudyWeb.Models;
using ScientificStudyWeb.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ScientificStudyWeb.Data;
using ScientificStudyWeb.DataObjects;
using ScientificStudyWeb.Services;

namespace ScientificStudyWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private readonly ScientificStudiesRecordDbContext _context;

        private UserService _userService;

        public LoginController(ScientificStudiesRecordDbContext context, IConfiguration configuration)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(_context);
            _userService = new UserService(configuration);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserDataObject userData)
        {
            var user = await _unitOfWork.userRepository.Login(userData.Username, userData.Password);

            if (user == null)
                return Unauthorized();


            var expires_in = DateTime.Now.AddMinutes(30);
            var temp = expires_in.ToLocalTime();

            var userDataToReturn = new UserDataToReturn()
            {
                AccessToken = _userService.GenerateJWTToken(user, expires_in),
                ExpiresAt = expires_in,
                User = new BasicUser()
                {
                    Username = user.Username,
                    Role = user.UserRole
                },
            };

            return Ok(userDataToReturn);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDataObject userData)
        {
            if (await _unitOfWork.userRepository.UserExists(userData.Username.ToLower()))
                return BadRequest("Username already exists");

            var userForCreation = new User
            {
                Username = userData.Username,
                UserRole = "User"
            };

            var createdUser = _userService.Register(userForCreation, userData.Password);
            _unitOfWork.userRepository.Add(createdUser);
            await _unitOfWork.SaveChangesAsync();

            if (createdUser == null)
                return BadRequest("Registering failed");

            return Ok();
        }
    }
}