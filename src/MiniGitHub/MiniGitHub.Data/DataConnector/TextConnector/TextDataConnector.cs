using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataAccessObjects;
using MiniGitHub.Data.DataAccessObjects.TextDAOs;

namespace MiniGitHub.Data.DataConnector;

public class TextDataConnector : IDataConnector {
    public TextDataConnector() {
        _userTextFile = "user.txt";
        _repositoryTextFile = "repository.txt";
        _directoryPath = GlobalConfig.GetTextFilePath();
    }

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

    private readonly string _directoryPath;
    private readonly string _userTextFile;
    private readonly string _repositoryTextFile;
}