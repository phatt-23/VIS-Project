using System.Data.Common;

namespace MiniGitHub.Domain.Entities;

public enum IssueStatus {
    Open, 
    Closed,
}

public class Issue {
    public long IssueId {get;set;}
    public long RepositoryId {get;set;}
    public long CreatorId {get;set;}
    public string Title {get;set;}
    public string Description {get;set;}
    public IssueStatus Status {get;set;}
    public DateTime CreatedAt {get;set;}
    public DateTime? ClosedAt {get;set;}
    
    public Repository Repository {get;set;}
    public User Creator {get;set;}
    public List<Comment> Comments {get;set;} = new List<Comment>();

    public Issue() {
    }
   
    public Issue(long issueId, long repositoryId, long creatorId, string title, string description, IssueStatus status) 
        :this(issueId, repositoryId, creatorId, title, description, status, DateTime.Now) {
    }
    
    
    public Issue(long issueId, long repositoryId, long creatorId, string title, string description, IssueStatus status, DateTime createdAt, DateTime? closedAt = null) {
        IssueId = issueId;
        RepositoryId = repositoryId;
        CreatorId = creatorId;
        Title = title;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
    }
}
