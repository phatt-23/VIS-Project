namespace MiniGitHub.Domain.Entities;

public class Commit {
    public int CommitId {get;set;}
    public int RepositoryId {get;set;}
    public string Message {get;set;}
    public DateTime CreatedAt {get;set;}
    public List<File> Files {get;set;}
}