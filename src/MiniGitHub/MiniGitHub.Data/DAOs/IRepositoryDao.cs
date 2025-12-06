using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs;

public interface IRepositoryDao {
    RepositoryRow? GetById(long repoId);
    List<RepositoryRow> GetByUserId(long userId);
    List<RepositoryRow> GetAll();
    RepositoryRow Insert(RepositoryRow repo);
    RepositoryRow? Update(RepositoryRow repo);
    bool Delete(long repoId);
}