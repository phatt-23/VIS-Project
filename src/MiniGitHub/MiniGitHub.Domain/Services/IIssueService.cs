using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Services;

public interface IIssueService {
    Issue? GetIssue(long issueId);
    Issue AddIssue(Issue issue);    
    bool RemoveIssue(long issueId);
    Issue? UpdateIssue(Issue issue);
    List<Issue> GetIssuesForRepo(long repoId);
}