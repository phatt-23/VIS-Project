using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Web.Models;

public class IssueListViewModel {
    public Repository Repo {get;set;}
    public List<Issue> Issues {get;set;}
    public string Query {get;set;} = string.Empty;
}