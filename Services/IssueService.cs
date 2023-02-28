using Microsoft.EntityFrameworkCore;
using Project_HU.Models;

namespace Project_HU.Services;

public class IssueService : IIssueService
{
    public TaskContext _context;

    public IssueService(TaskContext context)
    {
        _context = context;        
    }

    public ResponseModel CreateIssue(IssueDTO issueDTO)
    {
        ResponseModel model = new ResponseModel();

        try {
            User reporter = _context.Users.Find(issueDTO.reporter_id);
            User assignee = _context.Users.Find(issueDTO.assignee_id);
            Project project = _context.Projects.Find(issueDTO.project_id);

            Issue issue = new Issue(){
                title = issueDTO.title,
                description = issueDTO.description,
                Projects = project,
                Reporter = reporter,
                Assignee = assignee
            };
            _context.Add<Issue>(issue);
            model.Messsage = "Issue Created Successfully";
            _context.SaveChanges();
            model.IsSuccess = true;
        } catch (Exception e) {
            model.IsSuccess = false;
            model.Messsage = "Failed to create Issue. Error : " + e.Message;
        }

        return model;
    }

    public ResponseModel DeleteIssue(int issue_id)
    {
        ResponseModel model = new ResponseModel();
        try {
            Issue _temp = GetIssueById(issue_id);
            if (_temp != null) {
                _context.Remove < Issue > (_temp);
                _context.SaveChanges();
                model.IsSuccess = true;
                model.Messsage = "Issue Deleted Successfully";
            } else {
                model.IsSuccess = false;
                model.Messsage = "Issue Not Found";
            }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public List<Issue> GetAllIssues()
    {
        List<Issue> issues;
        try {
            issues = _context.Issues.
            Include(i => i.Projects).
            Include(i => i.Reporter).
            Include(i => i.Assignee).
            Include(i => i.Labels).
            ToList();
        } catch (Exception){
            throw;
        }
        return issues;
    }

    public Issue GetIssueById(int id)
    {
        Issue issue;
        try {
            issue = _context.Issues.
            Where(i => i.issue_id == id).
            Include(i => i.Projects).
            Include(i => i.Reporter).
            Include(i => i.Assignee).
            Include(i => i.Labels).
            FirstOrDefault();
        } catch (Exception) {
            throw;
        }
        return issue;
    }

    public List<Issue> GetIssuesByProjectIdOrCreator(int projectId, string assignee)
    {
        List<Issue> issue;
        try {
            issue = _context.Issues.
            Where(p => p.Assignee.email == assignee || p.Projects.project_id == projectId).ToList();
            return issue;
        } catch (Exception){
            throw;
        }
    }

    public List<Issue> SearchIssueByTitleOrDescription(string issue)
    {
        List<Issue> i;
        try {
            i = _context.Issues.
            Where(a => a.title == issue || a.description == issue).
            Include(i => i.Projects).
            Include(i => i.Reporter).
            Include(i => i.Assignee).
            Include(i => i.Labels).
            ToList();
        } catch (Exception) {
            throw;
        }
        return i;

    }

    public ResponseModel UpdateAssignee(int issue_id, int user_id)
    {
        ResponseModel model = new ResponseModel();
        try {
            User assignee = _context.Users.Find(user_id);
            if(assignee == null) {
                model.Messsage = "User does not exist";
                model.IsSuccess = false;
                return model;
            }
            Issue issue = _context.Issues.Find(issue_id);
            issue.Assignee = assignee;
            model.Messsage = "Assignee Updated";
            _context.SaveChanges();
            model.IsSuccess = true;
        } catch (Exception e) {
            model.Messsage = "Assignee Update Failed. Error : " + e.Message;
            model.IsSuccess = false;

        }
        return model;
    }

    public ResponseModel UpdateIssue(int issueId, IssueDTO issueDTO)
    {
        ResponseModel model = new ResponseModel();
        try {
            Issue _temp = GetIssueById(issueId);
            _temp.title = issueDTO.title;
            _temp.description = issueDTO.description;
            _temp.type = issueDTO.type;
            _temp.Projects = _context.Projects.Where(p => p.project_id == issueDTO.project_id).FirstOrDefault();
            _temp.Reporter = _context.Users.Where(r => r.user_id == issueDTO.reporter_id).FirstOrDefault();
            _temp.Assignee = _context.Users.Where(a => a.user_id == issueDTO.assignee_id).FirstOrDefault();

                model.Messsage = "Issue Updated Successfully";
                _context.SaveChanges();
                model.IsSuccess = true;
            } catch (Exception e) {
                model.IsSuccess = false;
                model.Messsage = "Failed to update Issue. Error : " + e.Message;
        }
 
        return model;
    }

    public ResponseModel UpdateStatus(int issue_id)
    {
        ResponseModel model = new ResponseModel();
        Issue issue = GetIssueById(issue_id);
        try {
            if(!(issue.status).Equals(5)){
                issue.status = issue.status + 1;
                model.Messsage = "Status Updated";
            }
            else {
                model.Messsage = "Status Already Up to Date";
            }
            _context.SaveChanges();
            model.IsSuccess = true;
        } catch (Exception e){
            model.IsSuccess = false;
            model.Messsage = "Status Update Failed. Error : " + e.Message;
        }

        return model;
    }
}
