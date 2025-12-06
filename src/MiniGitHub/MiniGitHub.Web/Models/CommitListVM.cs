using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Web.Models;

public class CommitListVM {
    public required Repository Repo {get;set;}
    public required List<Commit> Commits {get;set;}
}
