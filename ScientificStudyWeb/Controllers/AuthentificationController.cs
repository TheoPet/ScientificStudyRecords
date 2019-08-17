using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScientificStudiesRecord.Models;
using ScientificStudiesRecord.Data;
using ScientificStudiesRecord.DataObjects;

namespace ScientificStudiesRecord.Controllers
{
    [Route("master/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly IAuthentificationRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthentificationController(IAuthentificationRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthentificationDataObject userData)
        {
            var user = await _repository.Login(userData.Username, userData.Password);

            if (user == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var temp = Encoding.UTF8
                            .GetBytes(_configuration.GetSection("AppSettings:Token").Value);
             var key = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMonths(6),
                SigningCredentials = credentials

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthentificationDataObject userData)
        {
            userData.Username = userData.Username.ToLower();

            if (await _repository.UserExists(userData.Username))
                return BadRequest("Username already exists");

            var userForCreation = new User
            {
                Username = userData.Username
            };

            var createdUser = _repository.Register(userForCreation, userData.Password);
            
            if(createdUser == null)
                return BadRequest("Registering failed");

            return Ok();
        }
    }
}