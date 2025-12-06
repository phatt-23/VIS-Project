using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace MiniGitHub.Web.Models;

public class IssueAddDTO {
    [HiddenInput]
    public long RepositoryId {get;set;}
    
    [DisplayName("Title")]
    public string Title {get;set;} = null!;

    [DisplayName("Description")]
    public string Description {get;set;} = null!;
}