

using System.ComponentModel.DataAnnotations;

namespace Project_HU.Models;

public class Project{

    [Key]
    public int project_id { get; set; }

    public string description { get; set; }

    public List<User> Creator { get; set; }

}