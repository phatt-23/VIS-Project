using Microsoft.AspNetCore.Mvc;

namespace MiniGitHub.Web.Models;

public class RemoveRepoDTO {
    public required long RepositoryId {get;set;}
    public required string Name {get;set;}
    public required string Description {get;set;}
}