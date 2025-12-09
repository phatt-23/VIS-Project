using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Web.Models;

public class IssueDetailViewModel {
    public required Issue Issue {get;set;}

    public required CommentAddDTO AddCommentForm {get;set;}
}