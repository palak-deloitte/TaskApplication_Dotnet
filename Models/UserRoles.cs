using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
// using System.Text.Json.Serialization;

namespace Project_HU.Models;

public class UserRole {

    [Key]
    public int role_id { get; set; }

    public string role { get; set; }

    [JsonIgnore]
    public List<User> Users { get; set; }

}