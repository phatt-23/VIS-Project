using MiniGitHub.Data.DataAccessObjects;

namespace MiniGitHub.Data.DataConnector;

public class TextDataConnector : IDataConnector {
    public TextDataConnector(string textFilePath) {
        _textFilePath = textFilePath;
    }

    public IUserDao CreateUserDao() {
        throw new NotImplementedException();
    }

    public IRepositoryDao CreateRepositoryDao() {
        throw new NotImplementedException();
    }

    private readonly string _textFilePath;
}