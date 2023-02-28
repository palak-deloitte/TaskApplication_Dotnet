using Microsoft.EntityFrameworkCore;
using Project_HU.Models;

namespace Project_HU.Services;

public class ProjectService : IProjectService
{
    public TaskContext _context;

    public ProjectService(TaskContext context)
    {
        _context = context;        
    }

    public ResponseModel CreateIssue(int projectId, IssueDTO issueDTO)
    {
        ResponseModel model = new ResponseModel();

        try {
            Project proj = GetProjectById(projectId);
            User reporter = _context.Users.Find(issueDTO.reporter_id);
            User assignee = _context.Users.Find(issueDTO.assignee_id);

            Issue issue = new Issue(){
                title = issueDTO.title,
                description = issueDTO.description,
                type = issueDTO.type,
                Projects = proj,
                Assignee = assignee,
                Reporter = reporter
            };
            _context.Add<Issue>(issue);
            model.Messsage = "Issue Created Successfully under a project";
            _context.SaveChanges();
            model.IsSuccess = true;
        } catch (Exception e) {
            model.IsSuccess = false;
            model.Messsage = "Failed to create Issue under a project. Error : " + e.Message;
        }

        return model;
    }

    public ResponseModel CreateProject(ProjectDTO projectDTO)
    {
        ResponseModel model = new ResponseModel();

        try {
            User user = _context.Users.Find(projectDTO.user_id);
            Project proj = new Project(){
                description = projectDTO.description,
                Creator = user
            };
            _context.Add<Project>(proj);
            model.Messsage = "Project Created Successfully";
            _context.SaveChanges();
            model.IsSuccess = true;
        } catch (Exception e) {
            model.IsSuccess = false;
            model.Messsage = "Failed to create Project. Error : " + e.Message;
        }

        return model;
    }

    public ResponseModel DeleteIssue(int projectId, int issueId)
    {
        ResponseModel model = new ResponseModel();
        try {
            Issue _temp = _context.Issues.Find(issueId);
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

    public ResponseModel DeleteProject(int projectId)
    {
        ResponseModel model = new ResponseModel();
        try {
            Project _temp = GetProjectById(projectId);
            if (_temp != null) {
                _context.Remove < Project > (_temp);
                _context.SaveChanges();
                model.IsSuccess = true;
                model.Messsage = "Project Deleted Successfully";
            } else {
                model.IsSuccess = false;
                model.Messsage = "Project Not Found";
            }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public List<Project> GetAllProjects()
    {
        List<Project> project;
        try {
            project = _context.Projects.Include(s => s.Creator).ThenInclude(a => a.UserRoles).ToList();
        } catch (Exception){
            throw;
        }
        return project;

    }

    public List<Issue> GetIssuesByProjectId(int id)
    {
        List<Issue> issue;
        try {
            issue = _context.Issues.
            Where(a => a.Projects.project_id == id).
            Include(i => i.Projects).
            Include(i => i.Reporter).
            Include(i => i.Assignee).
            ToList();
        } catch (Exception) {
            throw;
        }
        return issue;
    }

    public Project GetProjectByCreator(string creator)
    {
        try {
            Project proj = _context.Projects.Where(p => p.Creator.email == creator).FirstOrDefault();
            return proj;
        } catch (Exception){
            throw;
        }
        
    }

    public Project GetProjectById(int id)
    {
        Project proj;
        try {
            proj = _context.Find<Project>(id);
        } catch (Exception) {
            throw;
        }
        return proj;
    }

    public ResponseModel UpdateProject(int projectId, ProjectDTO projectDTO)
    {

        ResponseModel model = new ResponseModel();
        try {
            Project _temp = GetProjectById(projectId);
            _temp.description = projectDTO.description;
                model.Messsage = "Project Updated Successfully";
                _context.SaveChanges();
                model.IsSuccess = true;
            } catch (Exception e) {
                model.IsSuccess = false;
                model.Messsage = "Failed to update Project. Error : " + e.Message;
        }
 
        return model;
    }
}