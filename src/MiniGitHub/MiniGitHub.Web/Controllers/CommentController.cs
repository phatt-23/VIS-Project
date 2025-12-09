using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Services;
using MiniGitHub.Web.Extensions;
using MiniGitHub.Web.Models;

namespace MiniGitHub.Web.Controllers;

public class CommentController(
    ICommentService commentService,
    IIssueService issueService
) : Controller {
    [HttpGet]
    public IActionResult Add(long issueId) {
        return RedirectToAction("Detail", "Issue", new {issueId});
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Add(CommentAddDTO dto, string action) {
        if (!ModelState.IsValid) {
            return RedirectToAction("Detail", "Issue", new {id = dto.IssueId});
        }

        if (!User.TryGetUserId(out long userId)) {
            return Forbid();
        }
        
        Issue? issue = issueService.GetIssue(dto.IssueId);
        if (issue == null) {
            return NotFound("Issue not found");
        }
        
        Comment comment = new Comment() {
            CommentId = -1,
            IssueId = issue.IssueId,
            AuthorId = userId,
            Content = dto.Content,
            CreatedAt = DateTime.Now,
        };


        try {
            switch (action) {
                case "comment":
                    commentService.Add(comment);
                    break;
                case "close":
                    if (!issueService.CloseIssue(issue.IssueId)) {
                        return Forbid("Unable to close issue.");
                    }
                    commentService.Add(comment);
                    break;
            }
        }
        catch (Exception e) {
            ModelState.AddModelError("", e.Message);
        }

        return RedirectToAction("Detail", "Issue", new {id = dto.IssueId});
    }
}