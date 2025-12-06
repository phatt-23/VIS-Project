using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs;

public interface IFileDao {
    FileRow? GetById(long fileId);
    List<FileRow> GetAll();
    FileRow Insert(FileRow row);
    FileRow? Update(FileRow row);
    bool Delete(long fileId);
    List<FileRow> GetByCommitId(long commitId);
}
