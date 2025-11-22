namespace MiniGitHub.Web.Models;

using File = MiniGitHub.Domain.Entities.File;

public class AddCommitDTO {
    public long RepositoryId {get;set;}
    public string Message {get;set;}
    public List<File> Files {get;set;} = new List<File>();
}