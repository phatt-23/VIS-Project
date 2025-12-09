using System.ComponentModel.DataAnnotations;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Web.Models;

public class CommitSearchForm {
    [Display(Name = "Search commits...")]
    public string Query {get;set;} = string.Empty;

    public enum OrderByOptions {
        Timestamp,
        Author,
    }
        
    public OrderByOptions OrderBy {get;set;} = OrderByOptions.Timestamp;

    public enum SortOptions {
        Ascending,
        Descending,
    }
        
    public SortOptions Sort {get;set;} = SortOptions.Descending;
}

public class CommitListVM {
    public required Repository Repo {get;set;}
    public required List<Commit> Commits {get;set;}
    public required CommitSearchForm SearchForm {get;set;}
}
