


using Project_HU.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_HU.Models;

namespace Project_HU.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 

public class ProjectController : ControllerBase {
    
    IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllProjects(){
        try {
            var proj = _projectService.GetAllProjects();
            if (proj == null) return NotFound();
            return Ok(proj);
        } catch(Exception e) {
            return BadRequest();
        }
    }


    [HttpGet]
    [Route("[action]")]
    public IActionResult GetProjectById(int id){
        try {
            var proj = _projectService.GetProjectById(id);
            return Ok(proj);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult CreateProject(ProjectDTO projectDTO) {
        try {
            var proj = _projectService.CreateProject(projectDTO);
            return Ok(proj);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult AssigngProjectCreator(ProjectUserDTO projectUserDTO) {
        try {
            var proj = _projectService.AssignProjectCreator(projectUserDTO);
            return Ok(proj);
        } catch( Exception e){
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]")]
    public IActionResult DeleteProject(int id) {
        try {
            var model = _projectService.DeleteProject(id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    public IActionResult UpdateProject(int id, ProjectDTO projectDTO){
        try {
            var proj = _projectService.UpdateProject(id, projectDTO);
            return Ok(proj);
        } catch (Exception){
            return BadRequest();
        }
    }
}