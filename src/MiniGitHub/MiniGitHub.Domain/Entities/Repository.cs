namespace MiniGitHub.Domain.Entities;

public enum Visibility {
    Public,
    Private,
}

public class Repository {
    public long RepositoryId {get;set;}
    public long OwnerId {get;set;}
    public string Name {get;set;}
    public string Description {get;set;}
    public bool IsPublic {get;set;}
    public DateTime CreatedAt {get;set;}
    public List<Commit> Commits {get;set;} = new List<Commit>();
    public User Owner {get;set;}
    
    public List<Issue> Issues {get;set;} = new List<Issue>();
    
    public List<Issue> OpenIssues => Issues.Where(i => i.Status == IssueStatus.Open).ToList();
    public List<Issue> ClosedIssues => Issues.Where(i => i.Status == IssueStatus.Closed).ToList();
    
    public Visibility Visibility => IsPublic ? Visibility.Public : Visibility.Private;

    public Repository() {
    }

    public Repository(long repositoryId, long ownerId, string name, string description, bool isPublic, DateTime createdAt) {
        RepositoryId = repositoryId;
        OwnerId = ownerId;
        Name = name;
        Description = description;
        IsPublic = isPublic;
        CreatedAt = createdAt;
        Commits = [];
    }

    public override string ToString() {
        return $"Repository( RepositoryId: {RepositoryId}, OwnerId: {OwnerId}, Name: {Name}, Description: {Description}, IsPublic: {IsPublic}, CreatedAt: {CreatedAt} )";
    }
}