using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DataAccessObjects;

public interface IIssueDao {
    IssueRow? GetById(long userId);
    List<IssueRow> GetAll();
    IssueRow Insert(IssueRow row);
    IssueRow Update(IssueRow row);
    bool Delete(long issueId);
}