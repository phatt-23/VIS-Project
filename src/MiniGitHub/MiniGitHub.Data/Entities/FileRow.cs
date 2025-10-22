namespace MiniGitHub.Data.Rows;

public class FileRow {
    public long FileId { get; set; }
    public long CommitId { get; set; }
    public string Path { get; set; }
    public string Content { get; set; }
}