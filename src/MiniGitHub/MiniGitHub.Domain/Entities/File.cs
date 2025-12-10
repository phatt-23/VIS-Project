namespace MiniGitHub.Domain.Entities;

public class File : IIdentifiableObject {
    public long Id {get;set;}
    public long CommitId {get;set;}
    public string Path {get;set;}
    public string Content {get;set;}

    public Commit Commit {get;set;} 

    public File() {
    }
    
    public File(long id, long commitId, string path, string content) {
        Id = id;
        CommitId = commitId;
        Path = path;
        Content = content;
    }

    public File(long id, string path, string content, Commit commit) 
        : this(id, commit.Id, path, content) 
    {
        Commit = commit;
    }

    public int SizeInBytes => Content?.Length ?? 0;
    public int SizeInKb => (Content?.Length ?? 0) / 1024;
    public string Extension => Path.Split('.').LastOrDefault() ?? "";

}