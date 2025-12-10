using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs;

public interface IFileDao : IDao<FileRow> {
    List<FileRow> GetByCommitId(long commitId);
}
