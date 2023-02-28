

using System.ComponentModel.DataAnnotations;

namespace Project_HU.Models;

public class Project{

    [Key]
    public int project_id { get; set; }

    public string description { get; set; }

    public User Creator { get; set; }

    public List<Issue> Issues { get; set; }


}