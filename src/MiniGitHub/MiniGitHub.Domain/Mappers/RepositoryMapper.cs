using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Mappers;

public class RepositoryMapper : IMapper<RepositoryRow, Repository>  {
    public Repository MapFromRow(RepositoryRow row) {
        return new Repository(row.RepositoryId, row.OwnerId, row.Name, row.Description, row.IsPublic, row.CreatedAt);
    }

    public RepositoryRow MapToRow(Repository repo) {
        return new RepositoryRow() {
            RepositoryId = repo.RepositoryId,
            OwnerId = repo.OwnerId,
            Name = repo.Name,
            Description = repo.Description,
            IsPublic = repo.IsPublic,
            CreatedAt = repo.CreatedAt,
        };
    }


}