

using System.ComponentModel.DataAnnotations;

namespace Project_HU.Models;

public class Issue{
    

    [Key]
    public int issue_id { get; set; }

    public string title { get; set; }

    public IssueType type { get; set; }

    public string description { get; set; }

}