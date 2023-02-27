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

    public ResponseModel AssignProjectCreator(ProjectUserDTO projectUserDTO)
    {
        ResponseModel model = new ResponseModel();
        var proj = _context.Projects.Where(a => a.project_id == projectUserDTO.project_id).Include(s => s.Creator).FirstOrDefault();

        var user = _context.Users.Find(projectUserDTO.user_id);

        try {
            proj.Creator.Add(user);
            _context.SaveChanges();
            model.Messsage = "Project Creator saved";
            model.IsSuccess = true;
        } catch (Exception e) {
            model.IsSuccess = false;
            model.Messsage = "Failed to add Creator. Error : " + e.Message;
        }

        return model;
    }

    public ResponseModel CreateProject(ProjectDTO projectDTO)
    {

        ResponseModel model = new ResponseModel();

        try {
            Project proj = new Project(){
                description = projectDTO.description
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
            // _context.Add<Project>(_temp);
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