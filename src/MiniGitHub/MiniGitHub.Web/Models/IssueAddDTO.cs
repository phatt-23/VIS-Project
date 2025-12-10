using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Web.Models;

public class IssueAddVM {
    public required Repository Repo {get;set;}
    public required IssueAddDTO Form {get;set;}
}

public class IssueAddDTO {
    [HiddenInput]
    public required long RepositoryId {get;set;}
    
    [DisplayName("Title")]
    public string Title {get;set;} = null!;

    [DisplayName("Description")]
    public string Description {get;set;} = null!;
}