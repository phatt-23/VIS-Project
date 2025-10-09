using MiniGitHub.Data;
using MiniGitHub.Data.DataAccessObjects;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.DataMappers;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Domain.Repositories;

public class RepositoryRepository : IRepositoryRepository {
    public RepositoryRepository() {
        _userDao = GlobalConfig.GetDataConnector().CreateUserDao();
        _repoDao = GlobalConfig.GetDataConnector().CreateRepositoryDao();
        _repoMapper = new RepositoryMapper(); 
        _userMapper = new UserMapper();
    }
    
    private readonly IUserDao _userDao;
    private readonly IRepositoryDao _repoDao;
    private readonly RepositoryMapper _repoMapper;
    private readonly UserMapper _userMapper;
}