using System.Data.Common;
using Microsoft.Extensions.Logging.Abstractions;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DataAccessObjects;

public class RepositorySqlDao : IRepositoryDao {
    public RepositorySqlDao(string connectionString) {
        _connectionString = connectionString;
    }

    public RepositoryRow? GetById(int repoId) {
        using (SqlDatabaseCall db = new SqlDatabaseCall(_connectionString)) {
            DbDataReader r = db.ExecuteReader(@"SELECT * FROM z_repository WHERE repository_id = @repository_id",
                new Dictionary<string, object>() {
                    {"@repository_id", repoId},
                });

            if (r.Read()) {
                return new RepositoryRow(r);
            }
        }

        return null;
    }

    public List<RepositoryRow> GetByUserId(int userId) {
        var rows = new List<RepositoryRow>();
        
        using (SqlDatabaseCall db = new SqlDatabaseCall(_connectionString)) {
            var r = db.ExecuteReader(@"SELECT r.* FROM z_repository r WHERE r.owner_id = @owner_id",
                new Dictionary<string, object>() {
                    {"@owner_id", userId},
                });
            
            while (r.Read()) {
                rows.Add(new RepositoryRow(r));
            }
        }

        return rows;
    }

    public List<RepositoryRow> GetAll() {
        var rows = new List<RepositoryRow>();
        
        using (SqlDatabaseCall db = new SqlDatabaseCall(_connectionString)) {
            var r = db.ExecuteReader(@"SELECT r.* FROM z_repository", new());
            while (r.Read()) {
                rows.Add(new RepositoryRow(r));
            }
        }

        return rows;
    }

    public RepositoryRow Insert(RepositoryRow row) {
        using (SqlDatabaseCall db = new SqlDatabaseCall(_connectionString)) {
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
                row.RepositoryId = (int)r;
                return row;
            }
        }

        throw new InvalidOperationException("Unable to insert new repository."); 
    }

    private readonly string _connectionString;
}
