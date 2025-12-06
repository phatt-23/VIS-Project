using System.ComponentModel.DataAnnotations;

namespace MiniGitHub.Web.Models;

public class AddRepoDTO {
    [Display(Name = "Name")]
    [DataType(DataType.Text)]
    public string Name {get;set;} = null!;
    
    [Display(Name = "Description")]
    [DataType(DataType.MultilineText)]
    public string Description {get;set;} = null!;
    
    [Display(Name = "Is Public")]
    public bool IsPublic {get;set;} = false;
}