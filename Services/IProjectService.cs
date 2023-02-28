

using Project_HU.Models;

namespace Project_HU.Services;

public interface IProjectService {
    List<Project> GetAllProjects();

    Project GetProjectById(int id);

    ResponseModel CreateProject(ProjectDTO projectDTO);
    
    ResponseModel DeleteProject(int projectId);

    ResponseModel UpdateProject(int projectId, ProjectDTO projectDTO);

    List<Issue> GetIssuesByProjectId(int id);

    ResponseModel CreateIssue(int projectId, IssueDTO issueDTO);

    ResponseModel DeleteIssue(int projectId, int issue_id);





}