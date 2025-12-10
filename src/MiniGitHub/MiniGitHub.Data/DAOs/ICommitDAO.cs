using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs;

public interface ICommitDao : IDao<CommitRow> {
    List<CommitRow> GetByRepoId(long repoId);
}