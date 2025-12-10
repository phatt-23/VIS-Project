using System.Data.Common;
using System.Transactions;
using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.Entities;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Mappers;
using IssueStatus = MiniGitHub.Data.Entities.IssueStatus;

namespace MiniGitHub.Domain.Services;

public class IssueService(IDataConnector connector) : IIssueService {
    private readonly IIssueDao _issueDao = connector.CreateIssueDao();
    private readonly IRepositoryDao _repoDao = connector.CreateRepositoryDao();
    private readonly ICommentDao _commentDao = connector.CreateCommentDao();
    private readonly IssueMapper _issueMapper = new();
    private readonly CommentMapper _commentMapper = new();
    
    public List<Issue> GetIssuesForRepo(long repoId) {
        RepositoryRow repoRow = _repoDao.GetById(repoId);

        List<IssueRow> issueRows = _issueDao.GetByRepoId(repoId);
        List<Issue> issues = issueRows.Select(_issueMapper.MapFromRow).ToList();

        return issues;
    }

    public Issue? GetIssue(long issueId) {
        try {
            return _issueMapper.MapFromRow(_issueDao.GetById(issueId));
        }
        catch {
            return null;
        }
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

    public bool CloseIssue(long issueId) {
        Issue? issue = GetIssue(issueId);
        if (issue == null) {
            return false;
        }

        issue.Status = Domain.Entities.IssueStatus.Closed;
        issue.ClosedAt = DateTime.Now;
        issue = UpdateIssue(issue);
        if (issue == null) {
            return false;
        }

        return true;
    }

    public bool CloseIssueWithComment(long issueId, Comment comment) {
        using (TransactionScope scope = new TransactionScope()) {
            if (CloseIssue(issueId)) {
                throw new Exception("Unable to close issue.");
            }
                
            _commentDao.Insert(_commentMapper.MapToRow(comment));
                
            scope.Complete();
            return true;
        }
    }
}