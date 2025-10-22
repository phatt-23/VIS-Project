using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Mappers;
using MiniGitHub.Domain.Repositories;

namespace MiniGitHub.Domain.Services.TransactionScriptPattern;

public class GetUserWithRepositoriesTS(IDataConnector connector) {

    public User? Run(long userId) {
        IUserDao userDao = connector.CreateUserDao();
        IRepositoryDao repoDao = connector.CreateRepositoryDao();
        
        UserRow? userRow = userDao.GetById(userId);
        if (userRow == null) {
            return null;
        }
        
        List<RepositoryRow> repoRows = repoDao.GetByUserId(userId);

        User user = _userMapper.MapFromRow(userRow);
        user.Repositories = repoRows.Select(_repoMapper.MapFromRow).ToList();

        return user;
    }

    // could also be injected, but they are stateless so no need 
    private readonly RepositoryMapper _repoMapper = new();
    private readonly UserMapper _userMapper = new();
}