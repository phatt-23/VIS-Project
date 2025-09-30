namespace MiniGitHub.Data.Rows;

public class CommentRow {
    public int CommentId {get;set;}
    public int IssueId {get;set;}
    public int AuthorId {get;set;}
    public string Content {get;set;}
    public DateTime CreatedAt {get;set;}
}