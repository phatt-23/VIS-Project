using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Mappers;

public class RepositoryMapper : IMapper<RepositoryRow, Repository>  {
    public Repository MapFromRow(RepositoryRow row) {
        return new Repository(row.Id, row.OwnerId, row.Name, row.Description, row.IsPublic, row.CreatedAt);
    }

    public RepositoryRow MapToRow(Repository repo) {
        return new RepositoryRow() {
            Id = repo.Id,
            OwnerId = repo.OwnerId,
            Name = repo.Name,
            Description = repo.Description,
            IsPublic = repo.IsPublic,
            CreatedAt = repo.CreatedAt,
        };
    }


}