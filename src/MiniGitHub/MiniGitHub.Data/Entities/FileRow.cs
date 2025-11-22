using System.Data.Common;
using MiniGitHub.Data.Extensions;

namespace MiniGitHub.Data.Rows;

public class FileRow {
    public long FileId { get; set; }
    public long CommitId { get; set; }
    public string Path { get; set; }
    public string Content { get; set; }

    public FileRow() {
    }
    
    public FileRow(long fileId, long commitId, string path, string content) {
        FileId = fileId;
        CommitId = commitId;
        Path = path;
        Content = content;
    }

    public FileRow(DbDataReader reader) {
        FileId = reader.Get<long>("file_id");
        CommitId = reader.Get<long>("commit_id");
        Path = reader.Get<string>("path");
        Content = reader.Get<string>("content");
    }
}