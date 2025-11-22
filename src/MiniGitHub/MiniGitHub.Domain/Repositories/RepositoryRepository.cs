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
        _commitDao = connector.CreateCommitDao();
        _repoMapper = new RepositoryMapper(); 
        _userMapper = new UserMapper();
        _commitMapper = new CommitMapper();
    }

    public List<Repository> GetAllRepos() {
        var repoRows = _repoDao.GetAll();
        return repoRows.Select(_repoMapper.MapFromRow).ToList();
    }

    public Repository? GetRepo(long repoId) {
        var row = _repoDao.GetById(repoId);
        
        if (row == null) {
            return null;
        }
        
        var repo = _repoMapper.MapFromRow(row);
        var commits = _commitDao.GetByRepositoryId(repoId).Select(_commitMapper.MapFromRow).ToList();
        repo.Commits = commits;
        
        return repo;
    }

    public Repository AddRepo(Repository repo) {
        var insertedRow = _repoDao.Insert(_repoMapper.MapToRow(repo)); 
        return _repoMapper.MapFromRow(insertedRow);
    }


    private readonly IUserDao _userDao;
    private readonly IRepositoryDao _repoDao;
    private readonly ICommitDao _commitDao;
    private readonly RepositoryMapper _repoMapper;
    private readonly UserMapper _userMapper;
    private readonly CommitMapper _commitMapper;
}