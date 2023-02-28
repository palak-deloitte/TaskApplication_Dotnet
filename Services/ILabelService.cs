

using Project_HU.Models;

namespace Project_HU.Services;

public interface ILabelService {

    ResponseModel AddLabelToIssue(int issueId, int labelId);
   
    ResponseModel RemoveLabel(int issueId, int labelId);

}