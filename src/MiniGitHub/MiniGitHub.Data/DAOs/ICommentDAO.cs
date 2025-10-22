using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs;

public interface ICommentDao {
    CommentRow? GetById(long commentId);
    List<CommentRow> GetAll();
    CommentRow Insert(CommentRow row);
    CommentRow Update(CommentRow row);
    bool Delete(long commentId);
}