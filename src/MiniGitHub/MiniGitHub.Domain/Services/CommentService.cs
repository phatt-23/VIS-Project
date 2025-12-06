using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DataConnector;
using MiniGitHub.Data.Rows;
using MiniGitHub.Domain.Entities;
using MiniGitHub.Domain.Mappers;

namespace MiniGitHub.Domain.Services;

public class CommentService(IDataConnector connector) : ICommentService {
    
    public List<Comment> GetCommentsForIssue(long issueId) {
        return _commentDao.GetAll()
            .Where(x => x.IssueId == issueId)
            .Select(_commentMapper.MapFromRow)
            .ToList();
    }

    public Comment Add(Comment comment) {
        CommentRow row = _commentDao.Insert(_commentMapper.MapToRow(comment)); 
        return _commentMapper.MapFromRow(row);
    }
    
    private readonly ICommentDao _commentDao = connector.CreateCommentDao();
    private readonly CommentMapper _commentMapper = new CommentMapper();
}