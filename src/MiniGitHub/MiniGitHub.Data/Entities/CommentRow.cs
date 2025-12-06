using System.Data.Common;
using MiniGitHub.Data.DataConnector.SqlConnector.Extensions;

namespace MiniGitHub.Data.Rows;

public class CommentRow {
    public long CommentId {get;set;}
    public long IssueId {get;set;}
    public long AuthorId {get;set;}
    public string Content {get;set;}
    public DateTime CreatedAt {get;set;}

    public CommentRow(DbDataReader reader) {
        CommentId = reader.Get<long>("comment_id");
        IssueId = reader.Get<long>("issue_id");
        AuthorId = reader.Get<long>("author_id");
        Content = reader.Get<string>("content");
        CreatedAt = reader.Get<DateTime>("created_at");
    }

    public CommentRow(long commentId, long issueId, long authorId, string content, DateTime createdAt) {
        CommentId = commentId;
        IssueId = issueId;
        AuthorId = authorId;
        Content = content;
        CreatedAt = createdAt;
    }
}