using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs;

public interface ICommitDao {
    CommitRow? GetById(long fileId);
    List<CommitRow> GetAll();
    CommitRow Insert(CommitRow row);
    CommitRow Update(CommitRow row);
    bool Delete(long fileId);
}