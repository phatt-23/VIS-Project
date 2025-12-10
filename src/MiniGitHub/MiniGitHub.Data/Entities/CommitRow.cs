using System.Data.Common;
using MiniGitHub.Data.DataConnector.SqlConnector.Extensions;
using MiniGitHub.Data.Entities;

namespace MiniGitHub.Data.Rows;

public class CommitRow : IIdentifialbeRow {
    public long Id {get;set;}
    public long RepositoryId {get;set;}
    public string Message {get;set;}
    public DateTime CreatedAt {get;set;}

    public CommitRow() {
        // just to a default constructor
    }

    public CommitRow(DbDataReader reader) {
        Id = reader.Get<long>("commit_id"); 
        RepositoryId = reader.Get<long>("repository_id"); 
        Message = reader.Get<string>("message"); 
        CreatedAt = reader.Get<DateTime>("created_at"); 
    }
}