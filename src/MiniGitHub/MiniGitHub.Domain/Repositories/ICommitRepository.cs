using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Repositories;

public interface ICommitRepository {
    List<Commit> GetCommitsForRepo(long repoId);
    Commit? GetCommit(long commitId);
    Commit AddCommit(Commit commit);
}