using MiniGitHub.Domain.Entities;
using File = MiniGitHub.Domain.Entities.File;

namespace MiniGitHub.Domain.Repositories;

public interface ICommitRepository {
    List<Commit> GetCommitsForRepo(long repoId);
    Commit? GetCommit(long commitId);
    Commit AddCommit(Commit commit, List<File> files);
}