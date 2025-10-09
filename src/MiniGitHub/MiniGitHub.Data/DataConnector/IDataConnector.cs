using MiniGitHub.Data.DataAccessObjects;

namespace MiniGitHub.Data.DataConnector;

public interface IDataConnector {
    IUserDao CreateUserDao();
    IRepositoryDao CreateRepositoryDao();
}