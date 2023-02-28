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

    public IssueController(IIssueService issueService)
    {
        _issueService = issueService;
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllIssues(){
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
        try {
            var assignee = _issueService.UpdateAssignee(issuueId, userId);
            return Ok(assignee);
        } catch (Exception){
            return BadRequest();
        }
    }
}