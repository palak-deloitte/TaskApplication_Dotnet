using Microsoft.EntityFrameworkCore;
using Project_HU.Models;

namespace Project_HU.Services;

public class LabelService : ILabelService
{
    public TaskContext _context;

    public LabelService(TaskContext context)
    {
        _context = context;        
    }

    public ResponseModel AddLabelToIssue(int issueId, int labelId)
    {
        ResponseModel model = new ResponseModel();
        var issue = _context.Issues.Where(a => a.issue_id == issueId).Include(i => i.Labels).FirstOrDefault();

        var label = _context.Labels.Find(labelId);

        try {
            issue.Labels.Add(label);
            _context.SaveChanges();
            model.Messsage = "Label Added";
            model.IsSuccess = true;
        } catch (Exception e) {
            model.IsSuccess = false;
            model.Messsage = "Failed to add Label. Error: " + e.Message;
        }

        return model;
    }

    public ResponseModel RemoveLabel(int issueId, int labelId)
    {
        ResponseModel model = new ResponseModel();
        try {
            var issue = _context.Issues.Where(a => a.issue_id == issueId).Include(i => i.Labels).FirstOrDefault();
            var label = _context.Labels.Find(labelId);
            issue.Labels.Remove(label);
            _context.SaveChanges();
            model.Messsage = "Label Removed";
            model.IsSuccess = true;
        } catch (Exception e) {
            model.IsSuccess = false;
            model.Messsage = "Failed to remove Label. Error : " + e.Message;
        }

        return model;
        
    }
}
