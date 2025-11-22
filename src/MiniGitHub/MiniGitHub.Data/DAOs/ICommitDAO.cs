using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs;

public interface ICommitDao {
    CommitRow? GetById(long commitId);
    List<CommitRow> GetAll();
    List<CommitRow> GetByRepositoryId(long repoId);
    CommitRow Insert(CommitRow row);
    CommitRow Update(CommitRow row);
    bool Delete(long commitId);
}