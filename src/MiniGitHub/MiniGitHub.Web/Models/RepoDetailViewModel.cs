using MiniGitHub.Domain.Entities;
using File = MiniGitHub.Domain.Entities.File;

namespace MiniGitHub.Web.Models;

public class RepoDetailViewModel {
    public required Repository Repo {get;set;}
    public required User Owner {get;set;}
    public required List<Commit> Commits {get;set;} = new List<Commit>();
    public required List<File> LatestFiles {get;set;} = new List<File>();
    public required List<Issue> Issues {get;set;} = new List<Issue>();
}