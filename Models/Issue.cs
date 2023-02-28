

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Project_HU.Models;

public class Issue{
    

    [Key]
    public int issue_id { get; set; }

    public string title { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public IssueType type { get; set; }

    public string description { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public IssueStatus status { get; set; }

    public Project Projects { get; set; }

    public User Reporter { get; set; }

    public User Assignee { get; set; }

    public List<Label> Labels { get; set; }




}