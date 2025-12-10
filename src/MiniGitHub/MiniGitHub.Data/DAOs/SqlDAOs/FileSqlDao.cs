using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs.SqlDAOs;

public class FileSqlDao(DbConnection connection) : IFileDao {
    
    public FileRow GetById(long fileId) {
        var call = new SqlDatabaseCall(connection);
        DbDataReader reader = call.ExecuteReader(
            "SELECT * FROM z_file WHERE file_id = @file_id",
            new() {
                {"@file_id", fileId}
            });

        if (reader.Read()) {
            return new FileRow(reader);
        }

        throw new Exception("File not found");
    }

    public List<FileRow> GetAll() {
        var call = new SqlDatabaseCall(connection);
        DbDataReader reader = call.ExecuteReader("SELECT * FROM z_file", new());

        var files = new List<FileRow>();
            while (reader.Read()) {
            files.Add(new FileRow(reader));
        }

        return files;
    }

    public FileRow Insert(FileRow row) {
        var call = new SqlDatabaseCall(connection);
        var id = call.ExecuteScalar(@"
                INSERT INTO z_file
                    (commit_id, path, content) 
                VALUES 
                    (@commit_id, @path, @content) 
                RETURNING file_id", 
            
            new() {
                {"@commit_id", row.CommitId}, 
                {"@path", row.Path}, 
                {"@content", row.Content}
            });

        if (id != null) {
            row.Id = (long)id;
            return row;
        }

        throw new InvalidOperationException("Unable to insert new file.");
    }

    public FileRow Update(FileRow row) {
        SqlDatabaseCall call = new SqlDatabaseCall(connection);

        int nrows = call.ExecuteNonQuery(@"
            UPDATE z_file 
            SET 
                path = @path, 
                content = @content 
            WHERE file_id = @file_id;", 
            
            new() {
            {"@file_id", row.Id}, 
            {"@path", row.Path}, 
            {"@content", row.Content}
        });

        if (nrows > 0) {
            return row;
        }
        
        throw new InvalidOperationException("Unable to update a file.");
    }

    public bool Delete(long fileId) {
        SqlDatabaseCall call = new SqlDatabaseCall(connection);

        int nrows = call.ExecuteNonQuery(@"
            DELETE FROM z_file 
            WHERE file_id = @file_id", 
            
            new() {
                {"@file_id", fileId}, 
            });

        if (nrows > 0) {
            return true;
        }

        return false;
    }

    public List<FileRow> GetByCommitId(long commitId) {
        SqlDatabaseCall call = new SqlDatabaseCall(connection);

        DbDataReader reader = call.ExecuteReader(@"
            SELECT * 
            FROM z_file 
            WHERE commit_id = @commit_id", 
            
            new() {
                {"@commit_id", commitId},
            });

        List<FileRow> rows = new List<FileRow>();
        while (reader.Read()) {
            rows.Add(new FileRow(reader));
        }

        return rows;         
    }
}