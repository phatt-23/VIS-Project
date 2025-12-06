using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Web.Models;

public enum SearchFilter {
    [Display(Name = "None")]
    None,
    
    [Display(Name = "Users")]
    Users,
    
    [Display(Name = "Repositories")]
    Repos,
    
    [Display(Name = "Users & Repositories")]
    All,
}

public class SearchDTO {
    public string Query {get;set;} = String.Empty;
    public SearchFilter Filter {get;set;} = SearchFilter.None;
    public List<User> Users {get;set;} = new List<User>();
    public List<Repository> Repos {get;set;} = new List<Repository>();
}