
namespace Project_HU.Models;


public class IssueDTO {
    public string title { get; set; }
    public string description { get; set; }

    public IssueType type { get; set; }

    public int project_id { get; set; }

    public int reporter_id { get; set; }

    public int assignee_id { get; set; }
}