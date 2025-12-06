using File = MiniGitHub.Domain.Entities.File;

namespace MiniGitHub.Domain.Services;

public interface IFileService {
    File AddFile(File file);
    List<File> GetByCommitId(long commitId);
    List<File> GetLatestCommittedFiles(long repoId);
}