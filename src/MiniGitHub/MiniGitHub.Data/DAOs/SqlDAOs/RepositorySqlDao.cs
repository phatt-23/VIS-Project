using System.Data.Common;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs.SqlDAOs;

public class RepositorySqlDao(DbConnection connection) : IRepositoryDao {
    
    public RepositoryRow GetById(long repoId) {
        var db = new SqlDatabaseCall(connection);
        
        DbDataReader r = db.ExecuteReader(@"SELECT * FROM z_repository WHERE repository_id = @repository_id",
            new() {
                {"@repository_id", repoId},
            });

        if (r.Read()) {
            return new RepositoryRow(r);
        }

        throw new Exception("Repository not found");
    }

    public List<RepositoryRow> GetAll() {
        var db = new SqlDatabaseCall(connection);
        var r = db.ExecuteReader(@"SELECT * FROM z_repository", new());
        
        var rows = new List<RepositoryRow>();
        while (r.Read()) {
            rows.Add(new RepositoryRow(r));
        }

        return rows;
    }

    public RepositoryRow Insert(RepositoryRow row) {
        var db = new SqlDatabaseCall(connection);
        
        var r = db.ExecuteScalar(@"
            INSERT INTO z_repository
                (owner_id, name, description, is_public, created_at) 
            VALUES 
                (@owner_id, @name, @description, @is_public, @created_at) 
            RETURNING repository_id",
            
            new() {
                {"@owner_id", row.OwnerId},
                {"@name", row.Name},
                {"@description", row.Description},
                {"@is_public", row.IsPublic},
                {"@created_at", row.CreatedAt},
            }
        );

        if (r is not null) {
            row.Id = (long)r;
            return row;
        }

        throw new InvalidOperationException("Unable to insert new repository."); 
    }

    public bool Delete(long repoId) {
        var db = new SqlDatabaseCall(connection);

        int nrows = db.ExecuteNonQuery(@"
             DELETE FROM z_repository 
             WHERE repository_id = @repository_id", 
            new() {
                {"@repository_id", repoId},
            });
        
        if (nrows > 0) {
            return true;
        }
        
        return false; 
    }

    public RepositoryRow Update(RepositoryRow repo) {
        var db = new SqlDatabaseCall(connection);

        int nrows = db.ExecuteNonQuery(@"
            UPDATE z_repository 
            SET 
                name=@name, 
                description=@description, 
                is_public=@is_public 
            WHERE repository_id=@repository_id",
            new() {
                {"@repository_id", repo.Id},
                {"@name", repo.Name},
                {"@description", repo.Description},
                {"@is_public", repo.IsPublic}
            });
        
        if (nrows > 0) {
            return repo;
        }
        
        throw new InvalidOperationException("Unable to update repository.");
    }
    
    public List<RepositoryRow> GetByUserId(long userId) {
        var rows = new List<RepositoryRow>();

        var db = new SqlDatabaseCall(connection);
        var r = db.ExecuteReader(@"SELECT r.* FROM z_repository r WHERE r.owner_id = @owner_id",
            new() {
                {"@owner_id", userId},
            });
        
        while (r.Read()) {
            rows.Add(new RepositoryRow(r));
        }

        return rows;
    }
}
