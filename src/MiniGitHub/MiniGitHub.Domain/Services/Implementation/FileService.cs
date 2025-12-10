using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Mappers;
using File = MiniGitHub.Domain.Entities.File;

namespace MiniGitHub.Domain.Services;

public class FileService(IDataConnector connector) : IFileService {
    public File AddFile(File file) {
        var inserted = _fileDao.Insert(_fileMapper.MapToRow(file));
        return _fileMapper.MapFromRow(inserted);
    }

    public List<File> GetByCommitId(long commitId) {
        var files = _fileDao.GetAll();
        return files.Where(f => f.CommitId == commitId).Select(_fileMapper.MapFromRow).ToList();
    }


    public List<File> GetLatestCommittedFiles(long repoId)
    {
        RepositoryRow repoRow = _repoDao.GetById(repoId);

        var commitRows = _commitDao.GetByRepoId(repoId); // get all commits for this repo

        if (commitRows.Count == 0) {
            return new List<File>();
        }

        var commitLookup = commitRows.ToDictionary(c => c.Id); // build a lookup 

        // collect all file rows for all commits
        var fileRows = commitRows
            .SelectMany(cr => _fileDao.GetByCommitId(cr.Id))
            .ToList();

        // map to domain objects
        var files = fileRows.Select(fr => {
            var commit = _commitMapper.MapFromRow(commitLookup[fr.CommitId]);
            return new File(fr.Id, fr.Path, fr.Content, commit);
        });

        // group per path and pick file with latest commit timestamp
        var latestFiles = files
            .GroupBy(f => f.Path)
            .Select(g => g
                .OrderByDescending(f => f.Commit.CreatedAt)
                .First())
            .ToList();

        return latestFiles;
    }

    private readonly ICommitDao _commitDao = connector.CreateCommitDao();
    private readonly IRepositoryDao _repoDao = connector.CreateRepositoryDao();
    private readonly IFileDao _fileDao = connector.CreateFileDao();
    private readonly CommitMapper _commitMapper = new();
    private readonly RepositoryMapper _repoMapper = new();
    private readonly FileMapper _fileMapper = new();
}