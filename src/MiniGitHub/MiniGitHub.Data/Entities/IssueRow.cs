using System.Data.Common;
using MiniGitHub.Data.DataConnector.SqlConnector.Extensions;

namespace MiniGitHub.Data.Entities;

public enum IssueStatus {
    Open = 0, 
    Closed = 1,
}

public static class IssueStatusMethods {

    public static string ToDatabaseString(this IssueStatus status) {
        return status switch {
            IssueStatus.Open => "open",
            IssueStatus.Closed => "closed",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null),
        };
    }

    public static IssueStatus ParseDatabaseString(string status) {
        return status switch {
            "open" => IssueStatus.Open,
            "closed" => IssueStatus.Closed,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null),
        };
    }
}

public class IssueRow {
    public long IssueId {get;set;}
    public long CreatorId {get;set;}
    public long RepositoryId {get;set;}
    public string Title {get;set;}
    public string Description {get;set;}
    public IssueStatus Status {get;set;}
    public DateTime CreatedAt {get;set;}
    public DateTime? ClosedAt {get;set;}

    public IssueRow() {
    }
    
    public IssueRow(long issueId, long repositoryId, long creatorId, string title, string description, IssueStatus status, DateTime createdAt, DateTime? closedAt = null) {
        IssueId = issueId;
        CreatorId = creatorId;
        RepositoryId = repositoryId;
        Title = title;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
        ClosedAt = closedAt;
    }

    public IssueRow(DbDataReader reader) {
        IssueId = reader.Get<long>("issue_id");
        CreatorId = reader.Get<long>("creator_id");
        RepositoryId = reader.Get<long>("repository_id");
        Title = reader.Get<string>("title");
        Description = reader.Get<string>("description");
        Status = IssueStatusMethods.ParseDatabaseString(reader.Get<string>("status"));
        CreatedAt = reader.Get<DateTime>("created_at");
        ClosedAt = reader.Get<DateTime?>("closed_at");
    }
    
    public override string ToString() {
        return $"Issue( IssueId: {IssueId}, Title: {Title}, Description: {Description}, CreatorId: {CreatorId}, Status: {Status}, CreatedAt: {CreatedAt} )";
    }
}