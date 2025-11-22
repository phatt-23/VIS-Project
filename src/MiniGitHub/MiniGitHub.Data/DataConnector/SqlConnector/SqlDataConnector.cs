using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using MiniGitHub.Data.DAOs;
using MiniGitHub.Data.DAOs.SqlDAOs;
using MiniGitHub.Data.DataAccessObjects;

namespace MiniGitHub.Data.DataConnector;

public class SqlDataConnector : IDataConnector, IDisposable {
    
    public SqlDataConnector() {
        SQLitePCL.Batteries.Init();
        
        _connection = new SqliteConnection(GlobalConfig.GetSqlConnectionString());
        _connection.Open();
    }

    public DbConnection GetConnection() {
        return _connection;
    }

    public void Dispose() {
        _connection.Dispose();
    }

    public IUserDao CreateUserDao() {
        return new UserSqlDao(_connection);
    }

    public IRepositoryDao CreateRepositoryDao() {
        return new RepositorySqlDao(_connection);
    }

    public ICommitDao CreateCommitDao() {
        return new CommitSqlDao(_connection);
    }

    public IFileDao CreateFileDao() {
        return new FileSqlDao(_connection);
    }

    private readonly SqliteConnection _connection;
}