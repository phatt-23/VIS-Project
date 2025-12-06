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
        return _userDao.GetByEmail(email) is not null; 
    }

    public bool UserWithUsernameExists(string username) {
        return _userDao.GetByUsername(username) is not null;
    }

    public User? GetUserByUsername(string username) {
        UserRow? userRow = _userDao.GetByUsername(username);
        return userRow is null ? null : _userMapper.MapFromRow(userRow);
    }

    public User? GetUserByEmail(string email) {
        UserRow? userRow = _userDao.GetByEmail(email); 
        return userRow is null ? null : _userMapper.MapFromRow(userRow);
    }

    public User? GetUserById(long userId) {
        UserRow? userRow = _userDao.GetById(userId);
        return userRow is null ? null : _userMapper.MapFromRow(userRow);
    }

    public User? UpdateUser(User user) {
        UserRow? userRow = _userDao.Update(_userMapper.MapToRow(user));
        return userRow is null ? null : _userMapper.MapFromRow(userRow);    
    }

    private readonly IUserDao _userDao = connector.CreateUserDao();
    private readonly IRepositoryDao _repoDao = connector.CreateRepositoryDao();
    private readonly UserMapper _userMapper = new();
    private readonly RepositoryMapper _repoMapper = new();
}