using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DataAccessObjects.TextDAOs;

public class RepositoryTextDao : IRepositoryDao {
    public RepositoryRow? GetById(int repoId) {
        throw new NotImplementedException();
    }

    public List<RepositoryRow> GetByUserId(int userId) {
        throw new NotImplementedException();
    }
}