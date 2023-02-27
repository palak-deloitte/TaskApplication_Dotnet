

using Project_HU.Models;

namespace Project_HU.Services;

public interface IProjectService {
    List<Project> GetAllProjects();

    Project GetProjectById(int id);

    ResponseModel CreateProject(ProjectDTO projectDTO);

    ResponseModel AssignProjectCreator(ProjectUserDTO projectUserDTO);
    
    ResponseModel DeleteProject(int projectId);

    ResponseModel UpdateProject(int projectId, ProjectDTO projectDTO);


}