using File = MiniGitHub.Domain.Entities.File;

namespace MiniGitHub.Domain.Repositories;

public interface IFileRepository {
    File AddFile(File file);
    List<File> GetByCommitId(long commitId);
}