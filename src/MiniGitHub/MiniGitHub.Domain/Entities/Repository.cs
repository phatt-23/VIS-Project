namespace MiniGitHub.Domain.Entities;

public class Repository {
    public int RepositoryId {get;set;}
    public int OwnerId {get;set;}
    public string Name {get;set;}
    public string Description {get;set;}
    public bool IsPublic {get;set;}
    public DateTime CreatedAt {get;set;}
    private List<Commit> Commits {get;set;} = new List<Commit>();
}