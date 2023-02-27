

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

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet("allUsers")]
    public IActionResult GetAllUsers() {
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
        try {
            var user = _userService.GetUserById(id);
            return Ok(user);
        } catch( Exception e){
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult SaveUser(UserRequest userRequest) {
        try {
            var user = _userService.SaveEmployee(userRequest);
            return Ok(user);
        } catch( Exception e){
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult AssignRole(UserWithRolesDTO userWithRolesDTO) {
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
        try {
            var proj = _userService.GetProjectByUserId(id);
            if (proj == null) return NotFound();
            return Ok(proj);
        } catch (Exception e) {
            return BadRequest();
        }
    }
}