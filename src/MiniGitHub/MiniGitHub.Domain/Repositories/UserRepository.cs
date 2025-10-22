using MiniGitHub.Data;
using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataAccessObjects;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Mappers;

namespace MiniGitHub.Domain.Repositories;

public class UserRepository : IUserRepository {
    public UserRepository(IDataConnector connector) {
        _userDao = connector.CreateUserDao();
        _repoDao = connector.CreateRepositoryDao();
        _userMapper = new UserMapper();
        _repoMapper = new RepositoryMapper();
    }
        
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
    
    private readonly IUserDao _userDao;
    private readonly IRepositoryDao _repoDao;
    private readonly UserMapper _userMapper;
    private readonly RepositoryMapper _repoMapper;
}