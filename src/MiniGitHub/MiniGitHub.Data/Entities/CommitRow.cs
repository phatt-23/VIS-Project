namespace MiniGitHub.Data.Rows;

public class CommitRow {
    public int CommitId {get;set;}
    public int RepositoryId {get;set;}
    public string Message {get;set;}
    public DateTime CreatedAt {get;set;}
}