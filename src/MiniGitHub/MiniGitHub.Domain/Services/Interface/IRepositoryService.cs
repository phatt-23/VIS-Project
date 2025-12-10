using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Services;

public interface IRepositoryService {
    List<Repository> GetAllRepos();
    Repository? GetRepo(long repoId);
    Repository? GetRepoWithOwner(long repoId);
    Repository? GetRepoWithCommits(long repoId);
    Repository AddRepo(Repository repo);
    bool RemoveRepo(long repoId);
    Repository? UpdateRepo(Repository repo);
}