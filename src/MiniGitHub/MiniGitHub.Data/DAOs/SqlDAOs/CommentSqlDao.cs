using System.Data.Common;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs.SqlDAOs;

file struct Sql {
    public const string GetById = "SELECT * FROM z_comment WHERE comment_id = @comment_id";
    public const string GetAll = "SELECT * FROM z_comment";
    public const string Insert = "INSERT INTO z_comment (issue_id, author_id, content, created_at) VALUES (@issue_id, @author_id, @content, @created_at)";   
    public const string Update = "UPDATE z_comment SET content = @content WHERE comment_id = @comment_id";
    public const string Delete = "DELETE FROM z_comment WHERE comment_id = @comment_id";
}

public class CommentSqlDao(DbConnection connection) : ICommentDao {
    public CommentRow GetById(long commentId) {
        SqlDatabaseCall call = new SqlDatabaseCall(connection);

        DbDataReader reader = call.ExecuteReader(Sql.GetById, new() {
            {"@comment_id", commentId},
        });

        if (reader.Read()) {
            return new CommentRow(reader);
        }

        throw new Exception("Comment not found");
    }

    public List<CommentRow> GetAll() {
        SqlDatabaseCall call = new SqlDatabaseCall(connection);

        DbDataReader reader = call.ExecuteReader(Sql.GetAll, new());

        List<CommentRow> rows = new List<CommentRow>();
        while (reader.Read()) {
            rows.Add(new CommentRow(reader));    
        }

        return rows;        
    }

    public CommentRow Insert(CommentRow row) {
        SqlDatabaseCall call = new SqlDatabaseCall(connection);

        object? rowId = call.ExecuteScalar(Sql.Insert, new() {
            {"@issue_id", row.IssueId},
            {"@author_id", row.AuthorId},
            {"@content", row.Content},
            {"@created_at", row.CreatedAt},
        });

        if (rowId is null) {
            throw new InvalidOperationException("Unable to insert new comment.");
        }

        row.Id = (long)rowId;
        return row;
    }

    public CommentRow Update(CommentRow row) {
        SqlDatabaseCall call = new SqlDatabaseCall(connection); 
        
        int nrows = call.ExecuteNonQuery(Sql.Update, new() {
            {"@content", row.Content},
            {"@comment_id", row.Id},
        });

        if (nrows <= 0) {
            throw new InvalidOperationException("Unable to update comment.");
        }
        
        return row;
    }

    public bool Delete(long commentId) {
        SqlDatabaseCall call = new(connection);

        int nrows = call.ExecuteNonQuery(Sql.Delete, new() {
            {"@comment_id", commentId},
        });

        if (nrows <= 0) {
            return false;
        }
        
        return true;
    }
}