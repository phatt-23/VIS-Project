namespace MiniGitHub.Data.Rows;

public enum IssueStatus {
    Open, 
    Closed,
}

public class IssueRow {
    public int IssueId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public IssueStatus Status { get; set; }
}