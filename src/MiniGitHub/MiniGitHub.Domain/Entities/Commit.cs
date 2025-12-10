namespace MiniGitHub.Domain.Entities;

public class Commit : IIdentifiableObject {
    public long Id {get;set;}
    public long RepositoryId {get;set;}
    public string Message {get;set;}
    public DateTime CreatedAt {get;set;}
    public List<File> Files {get;set;} = new List<File>();
    
    public Repository Repository {get;set;}

    public Commit(long id, long repositoryId, string message, DateTime createdAt) {
        Id = id;
        RepositoryId = repositoryId;
        Message = message;
        CreatedAt = createdAt;
    }
}