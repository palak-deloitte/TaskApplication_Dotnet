using Microsoft.AspNetCore.Mvc;
using Project_HU.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;  

namespace Project_HU.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;  

        public LoginController(IConfiguration _config)
        {
            _config= _config;
        }
        [HttpPost, Route("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.UserName) ||
                string.IsNullOrEmpty(loginDTO.Password))
                    return BadRequest("Username and/or Password not specified");
                if (loginDTO.UserName.Equals("palak") &&
                loginDTO.Password.Equals("palak@123"))
                {
                    var secretKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes("Thisismysecretkey"));
                    var signinCredentials = new SigningCredentials
                   (secretKey, SecurityAlgorithms.HmacSha256);
                    var jwtSecurityToken = new JwtSecurityToken(
                        "https://localhost:7261",  
                        "https://localhost:7261", 
                        claims:new List<Claim>(){new Claim("roles","admin")},
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: signinCredentials
                    );
                    return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
                }
            }
            catch
            {
                return BadRequest
                ("An error occurred in generating the token");
            }
            return Unauthorized();
        }
    }