using System.Data.Common;
using MiniGitHub.Data.DataConnector.SqlConnector.Extensions;
using MiniGitHub.Data.Entities;

namespace MiniGitHub.Data.Rows;

public class CommentRow : IIdentifialbeRow {
    public long Id {get;set;}
    public long IssueId {get;set;}
    public long AuthorId {get;set;}
    public string Content {get;set;}
    public DateTime CreatedAt {get;set;}

    public CommentRow() {
        
    } 
    
    public CommentRow(DbDataReader reader) {
        Id = reader.Get<long>("comment_id");
        IssueId = reader.Get<long>("issue_id");
        AuthorId = reader.Get<long>("author_id");
        Content = reader.Get<string>("content");
        CreatedAt = reader.Get<DateTime>("created_at");
    }

    public CommentRow(long id, long issueId, long authorId, string content, DateTime createdAt) {
        Id = id;
        IssueId = issueId;
        AuthorId = authorId;
        Content = content;
        CreatedAt = createdAt;
    }
}

