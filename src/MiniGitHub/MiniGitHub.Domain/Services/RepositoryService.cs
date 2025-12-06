using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Mappers;

namespace MiniGitHub.Domain.Services;

public class RepositoryService(IDataConnector connector) : IRepositoryService {
    public List<Repository> GetAllRepos() {
        List<RepositoryRow> repoRows = _repoDao.GetAll();
        return repoRows.Select(_repoMapper.MapFromRow).ToList();
    }

    public Repository? GetRepo(long repoId) {
        RepositoryRow? row = _repoDao.GetById(repoId);
        
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

    public bool RemoveRepo(long repoId) {
        return _repoDao.Delete(repoId); 
    }

    public Repository? UpdateRepo(Repository repo) {
        RepositoryRow repoRow = _repoMapper.MapToRow(repo);
        RepositoryRow? updatedRow = _repoDao.Update(repoRow);
        if (updatedRow is null) {
            return null;
        }
        return _repoMapper.MapFromRow(updatedRow);
    }

    public Repository? GetRepoWithOwner(long repoId) {
        Repository? repo = GetRepo(repoId);
        if (repo is null) {
            return null;
        }
        
        UserRow? ownerRow = _userDao.GetById(repo.OwnerId);
        if (ownerRow is null) {
            return null;
        }
        
        User user = _userMapper.MapFromRow(ownerRow);
        repo.Owner = user;
        
        return repo;
    }

    public Repository? GetRepoWithCommits(long repoId) {
        Repository? repo = GetRepo(repoId);
        if (repo is null) {
            return null;
        }

        List<CommitRow> commitRows = _commitDao.GetByRepositoryId(repo.RepositoryId);
        List<Commit> commits = commitRows.Select(_commitMapper.MapFromRow).ToList();

        repo.Commits = commits;
        
        return repo;
        
    }

    private readonly IUserDao _userDao = connector.CreateUserDao();
    private readonly IRepositoryDao _repoDao = connector.CreateRepositoryDao();
    private readonly ICommitDao _commitDao = connector.CreateCommitDao();
    private readonly RepositoryMapper _repoMapper = new();
    private readonly UserMapper _userMapper = new();
    private readonly CommitMapper _commitMapper = new();
}