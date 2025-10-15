using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DataAccessObjects;

public interface IRepositoryDao {
    RepositoryRow? GetById(int repoId);
    List<RepositoryRow> GetByUserId(int userId);
    List<RepositoryRow> GetAll();
    RepositoryRow Insert(RepositoryRow repo);
}