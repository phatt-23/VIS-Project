using MiniGitHub.Data;
using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataAccessObjects;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Mappers;

namespace MiniGitHub.Domain.Repositories;

public class RepositoryRepository : IRepositoryRepository {
    public RepositoryRepository(IDataConnector connector) {
        _userDao = connector.CreateUserDao();
        _repoDao = connector.CreateRepositoryDao();
        _repoMapper = new RepositoryMapper(); 
        _userMapper = new UserMapper();
    }
    
    private readonly IUserDao _userDao;
    private readonly IRepositoryDao _repoDao;
    private readonly RepositoryMapper _repoMapper;
    private readonly UserMapper _userMapper;
}