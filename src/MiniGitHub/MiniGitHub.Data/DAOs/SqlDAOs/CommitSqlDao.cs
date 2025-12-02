using System.Data.Common;
using Microsoft.Data.SqlClient;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs.SqlDAOs;

public class CommitSqlDao : ICommitDao {
    public CommitSqlDao(DbConnection connection) {
        _connection = connection;
    }
    
    public CommitRow? GetById(long commitId) {
        var db = new SqlDatabaseCall(_connection);
        
        DbDataReader r = db.ExecuteReader(
            @"SELECT * FROM z_commit WHERE commit_id = @commit_id",
            new Dictionary<string, object>() {
                {"@commit_id", commitId},
            });

        if (r.Read()) {
            return new CommitRow(r);
        }

        return null;
    }

    public List<CommitRow> GetAll() {
        var db = new SqlDatabaseCall(_connection);
        
        DbDataReader reader = db.ExecuteReader(@"SELECT * FROM z_commit", new());
        
        var commits = new List<CommitRow>();
        while (reader.Read()) {
            commits.Add(new CommitRow(reader));
        }

        return commits;
    }

    public List<CommitRow> GetByRepositoryId(long repoId) {
        var call = new SqlDatabaseCall(_connection);
        
        DbDataReader reader = call.ExecuteReader(
            @"SELECT * FROM z_commit WHERE repository_id = @repository_id",
            new Dictionary<string, object>() {
                {"@repository_id", repoId},
            });

        var commits = new List<CommitRow>();
        while (reader.Read()) {
            commits.Add(new CommitRow(reader)); 
        }

        return commits;
    }

    public CommitRow Insert(CommitRow row) {
        var db = new SqlDatabaseCall(_connection);
        var id = db.ExecuteScalar(
            @"INSERT INTO z_commit(message, repository_id, created_at) 
              VALUES (@message, @repository_id, @created_at) 
              RETURNING commit_id", 
            new() {
                {"@message", row.Message},
                {"@repository_id", row.RepositoryId},
                {"@created_at", row.CreatedAt},
            });

        if (id is not null) {
            row.CommitId = (long)id;
            return row;
        }
        
        throw new InvalidOperationException("Unable to insert new commit."); 
    }

    public CommitRow Update(CommitRow row) {
        var db = new SqlDatabaseCall(_connection);
        var id = db.ExecuteScalar(
            @"UPDATE z_commit 
              SET 
                  message = @message,
                  repository_id = @repository_id, 
                  created_at = @created_at
              RETURNING commit_id", 
            new() {
                {"@message", row.Message},
                {"@repository_id", row.RepositoryId},
                {"@created_at", row.CreatedAt},
            });

        if (id is not null) {
            return row;
        }
        
        throw new InvalidOperationException("Unable to update a commit."); 
    }

    public bool Delete(long commitId) {
        var db = new SqlDatabaseCall(_connection);
            
        string sql = @"DELETE FROM z_commit WHERE commit_id = @commit_id";
            
        Dictionary<string, object> ps = new Dictionary<string, object>() {
            {"@commit_id", commitId},
        };
            
        if (db.ExecuteNonQuery(sql, ps) > 0) {
            return true;
        }
            
        return false; 
    }

    private readonly DbConnection _connection;
}