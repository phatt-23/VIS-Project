using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Mappers;

public class CommitMapper : IMapper<CommitRow, Commit> {
    public CommitRow MapToRow(Commit model) {
        return new CommitRow() {
            Id = model.Id,
            RepositoryId = model.RepositoryId,
            Message = model.Message,
            CreatedAt = model.CreatedAt
        };
    }

    public Commit MapFromRow(CommitRow row) {
        return new Commit(row.Id, row.RepositoryId, row.Message, row.CreatedAt);
    }
}