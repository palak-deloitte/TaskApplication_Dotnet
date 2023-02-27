

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Project_HU.Models;

public class User{

    [Key]
    public int user_id { get; set; }

    public string username { get; set; }

    public string email { get; set; }

    public string password { get; set; }

    public List<UserRole> UserRoles { get; set; }

    [JsonIgnore]
    public List<Project> Projects { get; set; }

}