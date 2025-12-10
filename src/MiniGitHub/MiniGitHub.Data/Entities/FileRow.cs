using System.Data.Common;
using MiniGitHub.Data.DataConnector.SqlConnector.Extensions;
using MiniGitHub.Data.Entities;

namespace MiniGitHub.Data.Rows;

public class FileRow : IIdentifialbeRow {
    public long Id { get; set; }
    public long CommitId { get; set; }
    public string Path { get; set; }
    public string Content { get; set; }

    public FileRow() {
    }
    
    public FileRow(long id, long commitId, string path, string content) {
        Id = id;
        CommitId = commitId;
        Path = path;
        Content = content;
    }

    public FileRow(DbDataReader reader) {
        Id = reader.Get<long>("file_id");
        CommitId = reader.Get<long>("commit_id");
        Path = reader.Get<string>("path");
        Content = reader.Get<string>("content");
    }
}