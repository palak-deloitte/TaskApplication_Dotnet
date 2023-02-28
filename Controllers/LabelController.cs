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

    public LabelController(ILabelService labelService)
    {
        _labelService = labelService;
    }


    [HttpPost]
    [Route("[action]")]
    [Authorize(Roles="admin")]
    public IActionResult AddLabelToIssue(int issueId, int labelId) {
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
        try {
            var label = _labelService.RemoveLabel(issueId, labelId);
            return Ok(label);
        } catch (Exception e) {
            return BadRequest();
        }
    }
}
