using System.Data.Common;
using MiniGitHub.Data.DataConnector.SqlConnector.Extensions;

namespace MiniGitHub.Data.Rows;

public class CommitRow {
    public long CommitId {get;set;}
    public long RepositoryId {get;set;}
    public string Message {get;set;}
    public DateTime CreatedAt {get;set;}

    public CommitRow() {
        // just to a default constructor
    }

    public CommitRow(DbDataReader reader) {
        CommitId = reader.Get<long>("commit_id"); 
        RepositoryId = reader.Get<long>("repository_id"); 
        Message = reader.Get<string>("message"); 
        CreatedAt = reader.Get<DateTime>("created_at"); 
    }
}