namespace MiniGitHub.Domain.Entities;

public class Commit {
    public long CommitId {get;set;}
    public long RepositoryId {get;set;}
    public string Message {get;set;}
    public DateTime CreatedAt {get;set;}
    public List<File> Files {get;set;} = new List<File>();

    public Commit(long commitId, long repositoryId, string message, DateTime createdAt) {
        CommitId = commitId;
        RepositoryId = repositoryId;
        Message = message;
        CreatedAt = createdAt;
    }
}