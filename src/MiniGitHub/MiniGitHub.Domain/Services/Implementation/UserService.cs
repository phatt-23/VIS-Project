using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Mappers;

namespace MiniGitHub.Domain.Services;

public class UserService(IDataConnector connector) : IUserService {
    public List<User> GetAllUsers() {
        List<UserRow> userRows = _userDao.GetAll();
        List<User> users = userRows.Select(_userMapper.MapFromRow).ToList();
        return users;
    }
    
    public User? GetUserWithRepositories(long userId) {
        UserRow? userRow = _userDao.GetById(userId);
        if (userRow == null) {
            return null;
        }
        
        List<RepositoryRow> repoRows = _repoDao.GetByUserId(userId);

        User user = _userMapper.MapFromRow(userRow);
        user.Repositories = repoRows.Select(_repoMapper.MapFromRow).ToList();

        return user;
    }

    public User AddUser(User user) {
        UserRow row = _userMapper.MapToRow(user);
        row = _userDao.Insert(row);
        return _userMapper.MapFromRow(row);
    }

    public bool UserWithEmailExists(string email) {
        try {
            _userDao.GetByEmail(email);
            return true;
        }
        catch {
            return false;
        }
    }

    public bool UserWithUsernameExists(string username) {
        try {
            _userDao.GetByUsername(username);
            return true;
        }
        catch {
            return false;
        }
    }

    public User? GetUserByUsername(string username) {
        if (!UserWithUsernameExists(username)) {
            return null;
        }
        User user = _userMapper.MapFromRow(_userDao.GetByUsername(username));
        return user;
    }

    public User? GetUserByEmail(string email) {
        if (!UserWithEmailExists(email)) {
            return null;
        }
        User user = _userMapper.MapFromRow(_userDao.GetByEmail(email));
        return user;
    }

    public User? GetUserById(long userId) {
        try {
            return _userMapper.MapFromRow(_userDao.GetById(userId));
        }
        catch {
            return null;
        }
    }

    public User? UpdateUser(User user) {
        try {
            return _userMapper.MapFromRow(_userDao.Update(_userMapper.MapToRow(user)));
        }
        catch {
            return null;
        }
    }

    private readonly IUserDao _userDao = connector.CreateUserDao();
    private readonly IRepositoryDao _repoDao = connector.CreateRepositoryDao();
    private readonly UserMapper _userMapper = new();
    private readonly RepositoryMapper _repoMapper = new();
}