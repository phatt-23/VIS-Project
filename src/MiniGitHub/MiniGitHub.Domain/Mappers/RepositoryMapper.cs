using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.DataMappers;

public class RepositoryMapper {
    public Repository MapRepositoryRow(RepositoryRow row) {
        return new Repository {
            RepositoryId = row.RepositoryId,
            OwnerId = row.OwnerId,
            Name = row.Name,
            Description = row.Description,
            IsPublic = row.IsPublic,
            CreatedAt = row.CreatedAt,
        };
    }
}