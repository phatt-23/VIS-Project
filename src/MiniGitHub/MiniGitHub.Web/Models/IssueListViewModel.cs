using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Web.Models;

public class IssueListViewModel {
    public required Repository Repo {get;set;}
    public required List<Issue> Issues {get;set;}
    public string Query {get;set;} = string.Empty;
}