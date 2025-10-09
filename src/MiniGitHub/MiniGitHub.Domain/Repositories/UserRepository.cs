using MiniGitHub.Data;
using MiniGitHub.Data.DataAccessObjects;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.DataMappers;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Repositories;

public class UserRepository : IUserRepository {
    public UserRepository() {
        _userDao = GlobalConfig.GetDataConnector().CreateUserDao();
        _repoDao = GlobalConfig.GetDataConnector().CreateRepositoryDao();
        _userMapper = new UserMapper();
        _repoMapper = new RepositoryMapper();
    }
        
    public List<User> GetAllUsers() {
        List<UserRow> userRows = _userDao.GetAll();
        List<User> users = userRows.Select(_userMapper.MapUserRow).ToList();
        return users;
    }
    
    public User? GetUserWithRepositories(int userId) {
        UserRow? userRow = _userDao.GetById(userId);
        if (userRow == null) {
            return null;
        }
        
        List<RepositoryRow> repoRows = _repoDao.GetByUserId(userId);

        User user = _userMapper.MapUserRow(userRow);
        user.Repositories = repoRows.Select(r => _repoMapper.MapRepositoryRow(r)).ToList();

        return user;
    }
    
    private readonly IUserDao _userDao;
    private readonly IRepositoryDao _repoDao;
    private readonly UserMapper _userMapper;
    private readonly RepositoryMapper _repoMapper;
}