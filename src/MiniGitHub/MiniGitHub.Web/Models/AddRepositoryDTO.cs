namespace MiniGitHub.Web.Models;

public class AddRepositoryDTO {
    public long OwnerId {get;set;}
    public string Name {get;set;}
    public string Description {get;set;}
    public bool IsPublic {get;set;}
}