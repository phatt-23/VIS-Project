using System.ComponentModel.DataAnnotations;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Web.Models;

public class AddRepoDTO {
    [Display(Name = "Repository Name")]
    [DataType(DataType.Text)]
    public string Name {get;set;} = null!;
    
    [Display(Name = "Description")]
    [DataType(DataType.MultilineText)]
    public string Description {get;set;} = null!;
    
    [Display(Name = "Visibility")]
    public Visibility Visibility  {get;set;} = Visibility.Public;
}