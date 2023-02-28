using Project_HU.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_HU.Models;

namespace Project_HU.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 

public class LabelController : ControllerBase {
    
    ILabelService _labelService;
    private readonly ILogger<IssueController> _logger;


    public LabelController(ILabelService labelService, ILogger<IssueController> logger)
    {
        _labelService = labelService;
        _logger = logger;
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult AddLabelToIssue(int issueId, int labelId) {
        _logger.LogInformation("Adding Label to Issue...");
        try {
            var label = _labelService.AddLabelToIssue(issueId, labelId);
            return Ok(label);
        } catch( Exception e){
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]")]
    public IActionResult RemoveLabelFromIssue(int issueId, int labelId){
        _logger.LogInformation("Removing Label from Issue...");
        try {
            var label = _labelService.RemoveLabel(issueId, labelId);
            return Ok(label);
        } catch (Exception e) {
            return BadRequest();
        }
    }
}
