using System.Transactions;
using Microsoft.Data.SqlClient;
using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Mappers;
using File = MiniGitHub.Domain.Entities.File;

namespace MiniGitHub.Domain.Repositories;

public class CommitRepository : ICommitRepository {
    public CommitRepository(IDataConnector connector) {
        _connector = connector;
        _userDao = connector.CreateUserDao();
        _repoDao = connector.CreateRepositoryDao();
        _commitDao = connector.CreateCommitDao();
        _fileDao = connector.CreateFileDao();
        _repoMapper = new RepositoryMapper(); 
        _userMapper = new UserMapper();
        _commitMapper = new CommitMapper();
        _fileMapper = new FileMapper();
    }
    
    public List<Commit> GetCommitsForRepo(long repoId) {
        var commitRows = _commitDao.GetAll();
        return commitRows
            .Where(x => x.RepositoryId == repoId)
            .Select(_commitMapper.MapFromRow)
            .ToList();
    }

    public Commit? GetCommit(long commitId) {
        var row = _commitDao.GetById(commitId);
        if (row == null) {
            return null;
        }
        var commit = _commitMapper.MapFromRow(row);
        
        var files = _fileDao.GetAll().Where(x => x.CommitId == commitId).Select(_fileMapper.MapFromRow).ToList();
        commit.Files = files;

        return commit;
    }

    public Commit AddCommit(Commit commit, List<File> files) {
        try {
            _connector.BeginTransaction();

            var insertedRow = _commitDao.Insert(_commitMapper.MapToRow(commit));
            foreach (var file in files)
            {
                file.CommitId = insertedRow.CommitId;
                _fileDao.Insert(_fileMapper.MapToRow(file));
            }
            
            _connector.CommitTransaction();
            return _commitMapper.MapFromRow(insertedRow);
        }
        catch (Exception e) {
            _connector.RollbackTransaction();
            throw;
        }
    }

    private IDataConnector _connector;
    private IUserDao _userDao;
    private IRepositoryDao _repoDao;
    private ICommitDao _commitDao;
    private IFileDao _fileDao;
    private RepositoryMapper _repoMapper;
    private UserMapper _userMapper;
    private CommitMapper _commitMapper;
    private readonly FileMapper _fileMapper;
}