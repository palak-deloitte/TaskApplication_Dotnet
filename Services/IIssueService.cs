

using Project_HU.Models;

namespace Project_HU.Services;

public interface IIssueService {
    List<Issue> GetAllIssues();

    Issue GetIssueById(int id);

    ResponseModel CreateIssue(IssueDTO issueDTO);

    ResponseModel DeleteIssue(int issue_id);

    ResponseModel UpdateIssue(int issueId, IssueDTO issueDTO);

    ResponseModel UpdateStatus(int issue_id);

    ResponseModel UpdateAssignee(int issue_id, int user_id);

    List<Issue> SearchIssueByTitleOrDescription(string issue);

    List<Issue> GetIssuesByProjectIdOrCreator(int projectId, string creator);


}