

using System.ComponentModel.DataAnnotations;

namespace Project_HU.Models;

public class Label {
    [Key]
    public  int  id { get; set; }

    public string labelName { get; set; }

    public List<Issue> Issues { get; set; }

}