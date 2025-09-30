using System.Data.Common;
using Microsoft.Extensions.Logging.Abstractions;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DataAccessObjects;

public class RepositoryDao {
    public RepositoryDao() {
    }

    public RepositoryRow? GetById(int repoId) {
        using (Database db = new Database()) {
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
}