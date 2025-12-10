namespace MiniGitHub.Domain.Entities;

public class Comment : IIdentifiableObject {
    public required long Id {get;set;}
    public required long IssueId {get;set;}
    public required long AuthorId {get;set;}
    public required string Content {get;set;}
    public required DateTime CreatedAt {get;set;}

    public Issue Issue {get;set;} = null!;
    public User Author {get;set;} = null!;
}