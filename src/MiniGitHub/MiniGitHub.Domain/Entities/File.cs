namespace MiniGitHub.Domain.Entities;

public class File {
    public int FileId {get;set;}
    public int CommitId {get;set;}
    public string Path {get;set;}
    public string Content {get;set;}
}