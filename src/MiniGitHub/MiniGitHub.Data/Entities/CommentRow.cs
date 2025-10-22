namespace MiniGitHub.Data.Rows;

public class CommentRow {
    public long CommentId {get;set;}
    public long IssueId {get;set;}
    public long AuthorId {get;set;}
    public string Content {get;set;}
    public DateTime CreatedAt {get;set;}
}