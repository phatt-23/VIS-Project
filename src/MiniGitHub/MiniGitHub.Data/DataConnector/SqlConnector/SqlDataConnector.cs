using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using MiniGitHub.Data.DataAccessObjects;

namespace MiniGitHub.Data.DataConnector;

public class SqlDataConnector : IDataConnector {
    public IUserDao CreateUserDao() {
        return new UserSqlDao(_connectionString);
    }

    public IRepositoryDao CreateRepositoryDao() {
        return new RepositorySqlDao(_connectionString);
    }

    public SqlDataConnector(string connectionString) {
        _connectionString = connectionString;
        SQLitePCL.Batteries.Init();
    }

    public DbConnection CreateConnection() {
        return new SqliteConnection(_connectionString);
    }

    private readonly string _connectionString;
}