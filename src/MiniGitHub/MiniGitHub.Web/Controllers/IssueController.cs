using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Services;
using MiniGitHub.Web.Extensions;
using MiniGitHub.Web.Models;

namespace MiniGitHub.Web.Controllers;

public class IssueController(
    IRepositoryService repoService,    
    IIssueService issueService,
    ICommentService commentService,
    IUserService userService   
) : Controller {
    
    [HttpGet]
    public IActionResult List(long repoId) {
        Repository? repo = repoService.GetRepoWithOwner(repoId);
        if (repo is null) {
            return NotFound();
        }

        List<Issue> issues = issueService.GetIssuesForRepo(repo.RepositoryId);

        foreach (Issue issue in issues) {
            issue.Creator = userService.GetUserById(issue.CreatorId)!;
            issue.Comments = commentService.GetCommentsForIssue(issue.IssueId);
        }
        
        IssueListViewModel model = new IssueListViewModel() {
            Repo = repo,
            Issues = issues,
        };
        
        return View(model);
    }

    [HttpGet]
    public IActionResult Detail(long id) {
        Issue? issue = issueService.GetIssue(id);
        if (issue is null) {
            return NotFound();
        }
        
        issue.Comments = commentService.GetCommentsForIssue(issue.IssueId);
        issue.Creator = userService.GetUserById(issue.CreatorId)!;

        foreach (Comment comment in issue.Comments) {
            comment.Author = userService.GetUserById(comment.AuthorId)!;
        }

        IssueDetailViewModel model = new IssueDetailViewModel() {
            Issue = issue,
            AddCommentForm = new CommentAddDTO() {
                IssueId = issue.IssueId, 
            }
        };
        
        return View(model);
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult Add(long repoId) {
        Repository? repo = repoService.GetRepo(repoId);
        if (repo == null) {
            return NotFound("Repository not found");
        }

        if (User.TryGetUserId() != repo.OwnerId) {
            return Forbid();
        }
        
        IssueAddDTO dto = new IssueAddDTO() {
            RepositoryId = repoId,
        };
        
        return View(dto);
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Add(IssueAddDTO dto) {
        if (!ModelState.IsValid) {
            return View(dto);
        }
        
        Repository? repo = repoService.GetRepo(dto.RepositoryId);
        if (repo == null) {
            return NotFound("Repository not found");
        }

        if (User.TryGetUserId() != repo.OwnerId) {
            return Forbid();
        }

        Issue issue = new Issue(-1, repo.RepositoryId, User.GetUserId(), dto.Title, dto.Description, IssueStatus.Open); 
        
        issue = issueService.AddIssue(issue);
        
        return RedirectToAction("List", "Issue", new {repoId = dto.RepositoryId});
    }
}
