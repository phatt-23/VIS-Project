using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Repositories;

public interface IRepositoryRepository {
    List<Repository> GetAllRepos();
    Repository? GetRepo(long repoId);
    Repository AddRepo(Repository repo);
}