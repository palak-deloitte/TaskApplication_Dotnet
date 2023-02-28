


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

    private readonly ILogger<IssueController> _logger;


    public ProjectController(IProjectService projectService, ILogger<IssueController> logger)
    {
        _projectService = projectService;
        _logger = logger;
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllProjects(){
        _logger.LogInformation("Getting All Projects...");
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
        _logger.LogInformation("Getting Project by id...");
        try {
            var proj = _projectService.GetProjectById(id);
            return Ok(proj);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    [Authorize(Roles="Project Manager")]
    public IActionResult CreateProject(ProjectDTO projectDTO) {
        _logger.LogInformation("Creating Project...");
        try {
            var proj = _projectService.CreateProject(projectDTO);
            return Ok(proj);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]")]
    [Authorize(Roles="Project Manager")]
    public IActionResult DeleteProject(int id) {
        _logger.LogInformation("Deleting Project...");
        try {
            var model = _projectService.DeleteProject(id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    [Authorize(Roles="Project Manager")]
    public IActionResult UpdateProject(int id, ProjectDTO projectDTO){
        _logger.LogInformation("Updating Project...");
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
        _logger.LogInformation("Getting All Issues of a Project...");
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
    [Authorize(Roles="Project Manager")]
    public IActionResult CreateIssue(int projectId, IssueDTO issueDTO) {
        _logger.LogInformation("Creating Issue in a Project...");
        try {
            var issue = _projectService.CreateIssue(projectId, issueDTO);
            return Ok(issue);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]")]
    [Authorize(Roles="Project Manager")]
    public IActionResult DeleteIssue(int projectId, int issueId) {
        _logger.LogInformation("Deleting Issue in a Project...");
        try {
            var model = _projectService.DeleteIssue(projectId, issueId);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult SearchByIdOrCreator(int id=0, string creator = "creator"){
        _logger.LogInformation("Searching by Id or Creator...");

        if(id != 0){
            var proj = _projectService.GetProjectById(id);
            return Ok(proj);
        }
        else if(creator != null){
            var proj = _projectService.GetProjectByCreator(creator);
            return Ok(proj);
        }
        return BadRequest("Enter Correct Value");
    }
}