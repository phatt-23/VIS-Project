using MiniGitHub.Data.Entities;

namespace MiniGitHub.Data.DAOs;

public interface IIssueDao : IDao<IssueRow> {
    List<IssueRow> GetByRepoId(long repoId);
}