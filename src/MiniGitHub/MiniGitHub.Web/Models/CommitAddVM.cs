using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Web.Models;

public class CommitAddVM {
    public required AddCommitDTO Form {get;set;} 
    public required Repository Repo {get;set;}
}