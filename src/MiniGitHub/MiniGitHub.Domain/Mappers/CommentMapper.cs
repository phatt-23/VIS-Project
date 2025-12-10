using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Mappers;

public class CommentMapper : IMapper<CommentRow, Comment> {
    public CommentRow MapToRow(Comment model) {
        return new CommentRow(model.Id, model.IssueId, model.AuthorId, model.Content, model.CreatedAt);
    }

    public Comment MapFromRow(CommentRow row) {
        return new Comment() {
            Id = row.Id,
            IssueId = row.IssueId,
            AuthorId = row.AuthorId,
            Content = row.Content,
            CreatedAt = row.CreatedAt
        };
    }
}