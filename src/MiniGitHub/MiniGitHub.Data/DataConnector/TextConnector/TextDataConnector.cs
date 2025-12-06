using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DAOs.TextDAOs;
using MiniGitHub.Data.DataAccessObjects;
using MiniGitHub.Data.DataAccessObjects.TextDAOs;

namespace MiniGitHub.Data.DataConnector;

public class TextDataConnector : IDataConnector {

    public IUserDao CreateUserDao() {
        string path = _directoryPath + _userTextFile;
        return new UserTextDao(path);
    }

    public IRepositoryDao CreateRepositoryDao() {
        string path = _directoryPath + _repositoryTextFile;
        return new RepositoryTextDao(path);
    }

    public ICommitDao CreateCommitDao() {
        throw new NotImplementedException();
    }

    public IFileDao CreateFileDao() {
        throw new NotImplementedException(); 
    }

    public IIssueDao CreateIssueDao() {
        throw new NotImplementedException();
    }

    public ICommentDao CreateCommentDao() {
        throw new NotImplementedException();
    }

    public void BeginTransaction() {
        throw new NotImplementedException();
    }

    public void CommitTransaction() {
        throw new NotImplementedException();
    }

    public void RollbackTransaction() {
        throw new NotImplementedException();
    }

    private readonly string _directoryPath = GlobalConfig.GetTextFilePath();
    private const string _userTextFile = "user.txt";
    private const string _repositoryTextFile = "repository.txt";
}