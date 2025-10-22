using MiniGitHub.Data.DataAccessObjects;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs.SqlDAOs;

public class IssueDao : IIssueDao {
    public IssueRow? GetById(long userId) => throw new NotImplementedException();
    public List<IssueRow> GetAll() => throw new NotImplementedException();
    public IssueRow Insert(IssueRow row) => throw new NotImplementedException();
    public IssueRow Update(IssueRow row) => throw new NotImplementedException();
    public bool Delete(long issueId) => throw new NotImplementedException();
}