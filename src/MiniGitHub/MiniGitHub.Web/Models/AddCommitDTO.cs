using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Web.Models;

public class AddCommitDTO {
    [HiddenInput] 
    public long RepositoryId {get;set;}
    
    [Display(Name = "Message")]
    [DataType(DataType.MultilineText)]
    [Required(ErrorMessage = "Please enter a commit message.")]
    public string Message {get;set;} = string.Empty;
    
    [Display(Name = "Files")]
    [Required(ErrorMessage = "Please select at least one file.")]
    public IFormFileCollection Files {get;set;}
}