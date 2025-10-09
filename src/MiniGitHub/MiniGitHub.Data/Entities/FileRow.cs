namespace MiniGitHub.Data.Rows;

public class FileRow {
    public int FileId { get; set; }
    public int CommitId { get; set; }
    public string Path { get; set; }
    public string Content { get; set; }
}