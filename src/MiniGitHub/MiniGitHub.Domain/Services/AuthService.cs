using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Domain.Services;

public class AuthService(IDataConnector connector) : IAuthService {
    private readonly IUserDao _userDao = connector.CreateUserDao(); 
    
    public bool ValidateLogin(string username, string password) {
        UserRow? userRow = _userDao.GetByUsername(username);

        if (userRow is null) {
            return false;
        }

        if (userRow.Password != password) {
            return false;
        }

        return true;
    }
}