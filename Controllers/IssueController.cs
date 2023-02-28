using Project_HU.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_HU.Models;

namespace Project_HU.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 

public class IssueController : ControllerBase {
    
    IIssueService _issueService;

    private readonly ILogger<IssueController> _logger;

    public IssueController(IIssueService issueService, ILogger<IssueController> logger)
    {
        _issueService = issueService;
        _logger = logger;
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllIssues(){
        _logger.LogInformation("Getting All Issues...");
        try {
            var issue = _issueService.GetAllIssues();
            if (issue == null) return NotFound();
            return Ok(issue);
        } catch(Exception e) {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetIssueById(int id){
        _logger.LogInformation("Getting Issue By Id...");
        try {
            var issue = _issueService.GetIssueById(id);
            return Ok(issue);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult CreateIssue(IssueDTO issueDTO) {
        _logger.LogInformation("Creating Issue...");
        try {
            var issue = _issueService.CreateIssue(issueDTO);
            return Ok(issue);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]")]
    public IActionResult DeleteIssue(int id) {
        _logger.LogInformation("Deleting Issue...");
        try {
            var model = _issueService.DeleteIssue(id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    public IActionResult UpdateIssue(int id, IssueDTO issueDTO){
        _logger.LogInformation("Updating Issue...");
        try {
            var issue = _issueService.UpdateIssue(id, issueDTO);
            return Ok(issue);
        } catch (Exception){
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    public IActionResult UpdateStatus(int id){
        _logger.LogInformation("Updating Status of Issue...");
        try {
            var status = _issueService.UpdateStatus(id);
            return Ok(status);
        } catch (Exception){
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    public IActionResult UpdateAssignee(int issuueId, int userId){
        _logger.LogInformation("Updating Assignee of Issue...");
        try {
            var assignee = _issueService.UpdateAssignee(issuueId, userId);
            return Ok(assignee);
        } catch (Exception){
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult SearchIssueByTitleOrDescription(string issue){
        _logger.LogInformation("Searching Issue by Title or Description...");
        try {
            var i = _issueService.SearchIssueByTitleOrDescription(issue);
            return Ok(i);
        } catch (Exception) {
            return BadRequest();
        }
    }
}