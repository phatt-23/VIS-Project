using System.Data.Common;
using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataAccessObjects;

namespace MiniGitHub.Data.DataConnector;

public interface IDataConnector {
    IUserDao CreateUserDao();
    IRepositoryDao CreateRepositoryDao();
    ICommitDao CreateCommitDao();
    IFileDao CreateFileDao();
    IIssueDao CreateIssueDao();
    ICommentDao CreateCommentDao();

    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}