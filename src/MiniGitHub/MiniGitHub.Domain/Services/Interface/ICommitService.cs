using MiniGitHub.Domain.Entities;
using File = MiniGitHub.Domain.Entities.File;

namespace MiniGitHub.Domain.Services;

public interface ICommitService {
    List<Commit> GetCommitsForRepo(long repoId);
    Commit? GetCommit(long commitId);
    Commit AddCommit(Commit commit, IEnumerable<File> files);
}