using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DAOs.TextDAOs;

namespace MiniGitHub.Data.DataConnector;

public class TextDataConnector : IDataConnector {

    public IUserDao CreateUserDao() {
        return new UserTextDao(DirectoryPath + Filename.User);
    }

    public IRepositoryDao CreateRepositoryDao() {
        return new RepositoryTextDao(DirectoryPath + Filename.Repo);
    }

    public ICommitDao CreateCommitDao() {
        return new CommitTextDao(DirectoryPath + Filename.Commit);
    }

    public IFileDao CreateFileDao() {
        return new FileTextDao(DirectoryPath + Filename.File);
    }

    public IIssueDao CreateIssueDao() {
        return new IssueTextDao(DirectoryPath + Filename.Issue);
    }

    public ICommentDao CreateCommentDao() {
        return new CommentTextDao(DirectoryPath + Filename.Comment);
    }

    public void BeginTransaction() {
        return;
    }

    public void CommitTransaction() {
        return;
    }

    public void RollbackTransaction() {
        return;
    }

    private static readonly string DirectoryPath = GlobalConfig.GetTextFilePath();

    private struct Filename {
        public const string User = "user.json";
        public const string Repo = "repository.json";
        public const string Comment = "comment.json";
        public const string Commit = "commit.json";
        public const string File = "file.json";
        public const string Issue = "issue.json";
    }
}