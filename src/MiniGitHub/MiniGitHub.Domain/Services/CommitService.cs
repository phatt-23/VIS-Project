using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Mappers;
using File = MiniGitHub.Domain.Entities.File;

namespace MiniGitHub.Domain.Services;

public class CommitService(IDataConnector connector) : ICommitService {
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

    public Commit AddCommit(Commit commit, IEnumerable<File> files) {
        try {
            connector.BeginTransaction();

            var insertedRow = _commitDao.Insert(_commitMapper.MapToRow(commit));
            foreach (var file in files)
            {
                file.CommitId = insertedRow.CommitId;
                _fileDao.Insert(_fileMapper.MapToRow(file));
            }
            
            connector.CommitTransaction();
            return _commitMapper.MapFromRow(insertedRow);
        }
        catch (Exception e) {
            connector.RollbackTransaction();
            throw;
        }
    }

    private IUserDao _userDao = connector.CreateUserDao();
    private IRepositoryDao _repoDao = connector.CreateRepositoryDao();
    private ICommitDao _commitDao = connector.CreateCommitDao();
    private IFileDao _fileDao = connector.CreateFileDao();
    private RepositoryMapper _repoMapper = new();
    private UserMapper _userMapper = new();
    private CommitMapper _commitMapper = new();
    private readonly FileMapper _fileMapper = new();
}