using MiniGitHub.Data.Rows;
using File = MiniGitHub.Domain.Entities.File;

namespace MiniGitHub.Domain.Mappers;

public class FileMapper : IMapper<FileRow, File> {
    public FileRow MapToRow(File model) {
        return new FileRow() {
            FileId = model.FileId,
            CommitId = model.CommitId,
            Path = model.Path,
            Content = model.Content
        };
    }

    public File MapFromRow(FileRow row) {
        return new File(row.FileId, row.CommitId, row.Path, row.Content);
    }
}