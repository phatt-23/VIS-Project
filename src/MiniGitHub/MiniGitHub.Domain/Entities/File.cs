namespace MiniGitHub.Domain.Entities;

public class File {
    public long FileId {get;set;}
    public long CommitId {get;set;}
    public string Path {get;set;}
    public string Content {get;set;}
    public Commit Commit {get;set;}

    public File() {
    }
    
    public File(long fileId, long commitId, string path, string content) {
        FileId = fileId;
        CommitId = commitId;
        Path = path;
        Content = content;
    }

    public File(long fileId, string path, string content, Commit commit) 
        : this(fileId, commit.CommitId, path, content) 
    {
        Commit = commit;
    }
    
}