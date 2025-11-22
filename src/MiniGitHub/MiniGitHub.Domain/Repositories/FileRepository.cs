using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Domain.Mappers;
using File = MiniGitHub.Domain.Entities.File;

namespace MiniGitHub.Domain.Repositories;

public class FileRepository : IFileRepository {
    public FileRepository(IDataConnector connector) {
        _fileDao = connector.CreateFileDao();
        _fileMapper = new FileMapper();
    }

    public File AddFile(File file) {
        var inserted = _fileDao.Insert(_fileMapper.MapToRow(file));
        return _fileMapper.MapFromRow(inserted);
    }

    public List<File> GetByCommitId(long commitId) {
        var files = _fileDao.GetAll();
        return files.Where(f => f.CommitId == commitId).Select(_fileMapper.MapFromRow).ToList();
    }

    private readonly IFileDao _fileDao;
    private readonly FileMapper _fileMapper;
}