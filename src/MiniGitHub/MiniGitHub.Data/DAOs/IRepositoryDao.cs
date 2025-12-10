using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs;

public interface IRepositoryDao : IDao<RepositoryRow> {
    List<RepositoryRow> GetByUserId(long userId);
}