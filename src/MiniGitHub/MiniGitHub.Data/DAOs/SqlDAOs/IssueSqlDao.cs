using System.Data.Common;
using MiniGitHub.Data.Entities;

namespace MiniGitHub.Data.DAOs.SqlDAOs;

file struct Sql {
    public const string GetAll = "SELECT * FROM z_issue";
        
    public const string GetById = "SELECT * FROM z_issue WHERE issue_id = @issue_id";

    public const string Insert = """
        INSERT INTO z_issue 
            (repository_id, title, description, creator_id, status, created_at) 
        VALUES 
            (@repository_id, @title, @description, @creator_id, @status, @created_at)
        RETURNING issue_id
        """;

    public const string Update = """
        UPDATE z_issue 
        SET 
            title = @title, 
            description = @description,
            status = @status,
            closed_at = @closed_at
        WHERE issue_id = @issue_id
        """;
        
    public const string Delete = "DELETE FROM z_issue WHERE issue_id = @issue_id";
        
    public const string GetByRepoId = "SELECT * FROM z_issue WHERE repository_id = @repository_id";
}

public class IssueSqlDao(DbConnection connection) : IIssueDao {

    public IssueRow GetById(long userId) {
        SqlDatabaseCall call = new SqlDatabaseCall(connection);

        DbDataReader reader = call.ExecuteReader(Sql.GetById, new() {
            {"@issue_id", userId},
        });

        if (reader.Read()) {
            return new IssueRow(reader);
        }
        
        throw new Exception("Issue not found");
    }

    public List<IssueRow> GetAll() {
        SqlDatabaseCall call = new SqlDatabaseCall(connection);

        DbDataReader reader = call.ExecuteReader(Sql.GetAll, new());
        
        List<IssueRow> rows = new List<IssueRow>();
        while (reader.Read()) {
            rows.Add(new IssueRow(reader));
        }

        return rows;
    }
    
    public IssueRow Insert(IssueRow row) {
        SqlDatabaseCall call = new SqlDatabaseCall(connection);

        object? issueId = call.ExecuteScalar(Sql.Insert, new() {
            {"@repository_id", row.RepositoryId},
            {"@title", row.Title},
            {"@description", row.Description},
            {"@creator_id", row.CreatorId},
            {"@status", row.Status.ToDatabaseString()},
            {"@created_at", row.CreatedAt},
        });

        if (issueId is null) {
            throw new InvalidOperationException("Unable to insert new issue.");
        }
        
        row.Id = (long)issueId;

        return row;                  
    }

    public IssueRow Update(IssueRow row) {
        SqlDatabaseCall call = new SqlDatabaseCall(connection);

        int nrows = call.ExecuteNonQuery(Sql.Update, new() {
            {"@title", row.Title},
            {"@description", row.Description},
            {"@status", row.Status.ToDatabaseString()},
            {"@issue_id", row.Id},
            {"@closed_at", row.ClosedAt},
        });

        if (nrows <= 0) {
            throw new InvalidOperationException("Unable to update issue.");
        }
        
        return row;                      
    }

    public bool Delete(long issueId) {
        SqlDatabaseCall call = new SqlDatabaseCall(connection);

        int nrows = call.ExecuteNonQuery(Sql.Delete, new() {
            {"@issue_id", issueId},
        });

        if (nrows <= 0) {
            return false;
        }
        
        return true;                      
    }
    
    public List<IssueRow> GetByRepoId(long repoId) {
        SqlDatabaseCall call = new SqlDatabaseCall(connection);

        DbDataReader reader = call.ExecuteReader(Sql.GetByRepoId, new() {
            {"@repository_id", repoId},
        });
        
        List<IssueRow> rows = new List<IssueRow>();
        while (reader.Read()) {
            rows.Add(new IssueRow(reader));
        }

        return rows;         
    }
}