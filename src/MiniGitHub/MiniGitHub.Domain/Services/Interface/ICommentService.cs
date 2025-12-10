using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Services;

public interface ICommentService {
    List<Comment> GetCommentsForIssue(long issueId);
    Comment Add(Comment comment);
}