using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MiniGitHub.Web.Models;

public class EditRepoDTO {
    [HiddenInput] 
    public long RepositoryId {get;set;}
    
    [Display(Name = "Name")]
    [DataType(DataType.Text)]
    public string Name {get;set;} = null!;
    
    [Display(Name = "Description")]
    [DataType(DataType.MultilineText)]
    public string Description {get;set;} = null!;
    
    [Display(Name = "Is Public")]
    public bool IsPublic {get;set;} = false;
}