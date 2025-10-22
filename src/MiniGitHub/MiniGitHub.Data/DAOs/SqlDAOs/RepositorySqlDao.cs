using System.Data.Common;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs.SqlDAOs;

public class RepositorySqlDao : IRepositoryDao {
    public RepositorySqlDao(DbConnection connection) {
        _connection = connection;
    }

    public RepositoryRow? GetById(long repoId) {
        var db = new SqlDatabaseCall(_connection);
        
        DbDataReader r = db.ExecuteReader(@"SELECT * FROM z_repository WHERE repository_id = @repository_id",
            new Dictionary<string, object>() {
                {"@repository_id", repoId},
            });

        if (r.Read()) {
            return new RepositoryRow(r);
        }

        return null;
    }

    public List<RepositoryRow> GetByUserId(long userId) {
        var rows = new List<RepositoryRow>();

        var db = new SqlDatabaseCall(_connection);
        var r = db.ExecuteReader(@"SELECT r.* FROM z_repository r WHERE r.owner_id = @owner_id",
            new Dictionary<string, object>() {
                {"@owner_id", userId},
            });
        
        while (r.Read()) {
            rows.Add(new RepositoryRow(r));
        }

        return rows;
    }

    public List<RepositoryRow> GetAll() {
        var db = new SqlDatabaseCall(_connection);
        var r = db.ExecuteReader(@"SELECT * FROM z_repository", new());
        
        var rows = new List<RepositoryRow>();
        while (r.Read()) {
            rows.Add(new RepositoryRow(r));
        }

        return rows;
    }

    public RepositoryRow Insert(RepositoryRow row) {
        var db = new SqlDatabaseCall(_connection);
        
        var r = db.ExecuteScalar(
            @"INSERT INTO z_repository(owner_id, name, description, is_public, created_at) 
                  VALUES (@owner_id, @name, @description, @is_public, @created_at) 
                  RETURNING repository_id",
            new Dictionary<string, object>() {
                {"@owner_id", row.OwnerId},
                {"@name", row.Name},
                {"@description", row.Description},
                {"@is_public", row.IsPublic},
                {"@created_at", row.CreatedAt},
            }
        );

        if (r is not null) {
            row.RepositoryId = (long)r;
            return row;
        }

        throw new InvalidOperationException("Unable to insert new repository."); 
    }

    public bool Delete(long repoId) {
        var db = new SqlDatabaseCall(_connection);
        
        string sql = @"
             DELETE FROM z_repository 
             WHERE repository_id = @repository_id
             ";
        
        Dictionary<string, object> ps = new Dictionary<string, object>() {
            {"@repository_id", repoId},
        };
        
        if (db.ExecuteNonQuery(sql, ps) > 0) {
            return true;
        }
        
        return false; 
    }

    private readonly DbConnection _connection;
}
