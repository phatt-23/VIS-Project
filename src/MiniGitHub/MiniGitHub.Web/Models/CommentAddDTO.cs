namespace MiniGitHub.Web.Models;

public class CommentAddDTO {
    public required long IssueId {get;set;}
    public string Content {get;set;} 
}