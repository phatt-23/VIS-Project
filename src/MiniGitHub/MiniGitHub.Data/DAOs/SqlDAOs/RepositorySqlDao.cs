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

    private readonly string _connectionString;
}
