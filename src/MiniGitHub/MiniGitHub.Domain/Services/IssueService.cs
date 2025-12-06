using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataAccessObjects;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.Entities;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Mappers;

namespace MiniGitHub.Domain.Services;

public class IssueService(IDataConnector connector) : IIssueService {
    private readonly IIssueDao _issueDao = connector.CreateIssueDao();
    private readonly IRepositoryDao _repoDao = connector.CreateRepositoryDao();
    private readonly IssueMapper _issueMapper = new IssueMapper();
    
    public List<Issue> GetIssuesForRepo(long repoId) {
        RepositoryRow? repoRow = _repoDao.GetById(repoId);
        if (repoRow is null) {
            return new List<Issue>();
        }

        List<IssueRow> issueRows = _issueDao.GetByRepoId(repoId);
        List<Issue> issues = issueRows.Select(_issueMapper.MapFromRow).ToList();

        return issues;
    }

    public Issue? GetIssue(long issueId) {
        IssueRow? row = _issueDao.GetById(issueId);
        if (row is null) {
            return null;
        }
        return _issueMapper.MapFromRow(row);
    }

    public Issue AddIssue(Issue issue) {
        IssueRow row = _issueMapper.MapToRow(issue);
        row = _issueDao.Insert(row);
        return _issueMapper.MapFromRow(row);
    }

    public bool RemoveIssue(long issueId) {
        return _issueDao.Delete(issueId);
    }

    public Issue? UpdateIssue(Issue issue) {
        IssueRow row = _issueMapper.MapToRow(issue);
        row = _issueDao.Update(row);
        return _issueMapper.MapFromRow(row);        
    }
}