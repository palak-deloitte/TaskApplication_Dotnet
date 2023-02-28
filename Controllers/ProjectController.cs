


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

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetIssuesOfProject(int id){
        try {
            var issue = _projectService.GetIssuesByProjectId(id);
            if (issue == null) return NotFound();
            return Ok(issue);
        } catch (Exception e) {
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult CreateIssue(int projectId, IssueDTO issueDTO) {
        try {
            var issue = _projectService.CreateIssue(projectId, issueDTO);
            return Ok(issue);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]")]
    public IActionResult DeleteIssue(int projectId, int issueId) {
        try {
            var model = _projectService.DeleteIssue(projectId, issueId);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult SearchByIdOrCreator([FromQuery]int id=0, [FromQuery]ProjectDTO projectDTO=null){
        if(id != 0){
            var proj = _projectService.GetProjectById(id);
            return Ok(proj);
        }
        else if(projectDTO != null){
            var proj = _projectService.GetProjectByCreator(projectDTO);
            return Ok(proj);
        }
        return BadRequest("Enter Correct Value");
    }
}