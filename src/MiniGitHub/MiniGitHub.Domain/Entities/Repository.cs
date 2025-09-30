namespace MiniGitHub.Domain.Entities;

public class Repository {
    public int RepositoryId {get;set;}
    public int OwnerId {get;set;}
    public string Title {get;set;}
}