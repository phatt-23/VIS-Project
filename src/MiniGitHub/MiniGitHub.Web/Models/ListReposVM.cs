using System.ComponentModel;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Web.Models;

public class SearchRepoForm {
    [DisplayName("Find a repository...")]
    public string Query {get;set;} = string.Empty;
}

public class ListReposVM {
    public required User User {get;set;} 
    public required List<Repository> Repos {get;set;}
    public required SearchRepoForm SearchForm {get;set;}
}