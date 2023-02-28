

using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_HU.Services;
using Project_HU.Models;

namespace Project_HU.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 

public class UserController : ControllerBase {
    IUserService _userService;

    private readonly ILogger<IssueController> _logger;


    public UserController(IUserService userService, ILogger<IssueController> logger)
    {
        _userService = userService;
        _logger = logger;
    }


    [HttpGet("allUsers")]
    public IActionResult GetAllUsers() {
        _logger.LogInformation("Getting All Users...");
        try {
            var users = _userService.GetUsers();
            if (users == null) return NotFound();
            return Ok(users);
        } catch (Exception e) {
            return BadRequest();
        }
    }


    [HttpGet("getUserById")]
    public IActionResult GetUserById(int id) {
        _logger.LogInformation("Getting User By Id...");
        try {
            var user = _userService.GetUserById(id);
            return Ok(user);
        } catch( Exception e){
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    [Authorize(Roles="admin")]
    public IActionResult SaveUser(UserRequest userRequest) {
        _logger.LogInformation("Adding a User...");
        try {
            var user = _userService.SaveEmployee(userRequest);
            return Ok(user);
        } catch( Exception e){
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    [Authorize(Roles="admin")]
    public IActionResult AssignRole(UserWithRolesDTO userWithRolesDTO) {
        _logger.LogInformation("Assigning Role to User...");
        try {
            var user = _userService.AssignRole(userWithRolesDTO);
            return Ok(user);
        } catch( Exception e){
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetProjectsOfUser(int id){
        _logger.LogInformation("Getting All Projects of a User...");
        try {
            var proj = _userService.GetProjectByUserId(id);
            if (proj == null) return NotFound();
            return Ok(proj);
        } catch (Exception e) {
            return BadRequest();
        }
    }
}