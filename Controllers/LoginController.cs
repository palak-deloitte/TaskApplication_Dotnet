using Microsoft.AspNetCore.Mvc;
using Project_HU.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Project_HU.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private IConfiguration _config;

    TaskContext _taskContext;

    public LoginController(IConfiguration _config, TaskContext taskContext)
    {
        _config = _config;
        _taskContext = taskContext;
    }
    [HttpPost, Route("login")]
    public IActionResult Login(LoginDTO loginDTO)
    {
        try
        {
            User user = _taskContext.Users.FirstOrDefault(a => a.username == loginDTO.UserName);
            if (user == null) return BadRequest();
            if (user.password == loginDTO.Password)
            {


                User u = _taskContext.Users.Include(u => u.UserRoles).FirstOrDefault(a => a.user_id == user.user_id);

                List<Claim> claim = new List<Claim>();

                if (u.UserRoles == null || u.UserRoles.Count == 0)
                {
                    return BadRequest("Please add Role");
                }

                foreach (var temp in u.UserRoles)
                {
                    claim.Add(new Claim("roles", temp.role));
                }

                var secretKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes("Thisismysecretkey"));
                var signinCredentials = new SigningCredentials
               (secretKey, SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                    "https://localhost:7261",
                    "https://localhost:7261",
                    claims: claim,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: signinCredentials
                );
                return Ok(new JwtSecurityTokenHandler().
                WriteToken(jwtSecurityToken));

            }
            else
            {
                return BadRequest("Wrong Paasword");
            }
        }
        catch
        {
            return BadRequest
            ("An error occurred in generating the token");
        }
    }
}