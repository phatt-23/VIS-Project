namespace MiniGitHub.Data.Rows;

public class CommitRow {
    public long CommitId {get;set;}
    public long RepositoryId {get;set;}
    public string Message {get;set;}
    public DateTime CreatedAt {get;set;}
}