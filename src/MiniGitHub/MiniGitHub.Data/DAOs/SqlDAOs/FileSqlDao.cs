using System.Data;
using System.Data.Common;
using MiniGitHub.Data.Rows;

namespace MiniGitHub.Data.DAOs.SqlDAOs;

public class FileSqlDao : IFileDao {
    public FileSqlDao(DbConnection connection) {
        _connection = connection;
    }

    public FileRow? GetById(long fileId) {
        var call = new SqlDatabaseCall(_connection);
        DbDataReader reader = call.ExecuteReader(
            "SELECT * FROM z_file WHERE file_id = @file_id",
            new() {
                {"@file_id", fileId}
            });

        if (reader.Read()) {
            return new FileRow(reader);
        }

        return null;
    }

    public List<FileRow> GetAll() {
        var call = new SqlDatabaseCall(_connection);
        DbDataReader reader = call.ExecuteReader("SELECT * FROM z_file", new());

        var files = new List<FileRow>();
            while (reader.Read()) {
            files.Add(new FileRow(reader));
        }

        return files;
    }

    public FileRow Insert(FileRow row) {
        var call = new SqlDatabaseCall(_connection);
        var id = call.ExecuteScalar(
            @"INSERT INTO z_file(commit_id, path, content) 
              VALUES (@commit_id, @path, @content) 
              RETURNING file_id", 
            new() {
                {"@commit_id", row.CommitId}, 
                {"@path", row.Path}, 
                {"@content", row.Content}
            });

        if (id is not null) {
            row.FileId = (long)id;
            return row;
        }

        throw new InvalidOperationException("Unable to insert new file.");
    }

    public FileRow Update(FileRow row) => throw new NotImplementedException();

    public bool Delete(long fileId) => throw new NotImplementedException();

    private readonly DbConnection _connection;
}