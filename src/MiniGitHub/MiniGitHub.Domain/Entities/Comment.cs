namespace MiniGitHub.Domain.Entities;

public class Comment {
    public required long CommentId {get;set;}
    public required long IssueId {get;set;}
    public required long AuthorId {get;set;}
    public required string Content {get;set;}
    public required DateTime CreatedAt {get;set;}
   
    public Issue Issue {get;set;} 
    public User Author {get;set;}
}