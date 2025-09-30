using System.Data;
using System.Data.Common;
using MiniGitHub.Data.Extensions;

namespace MiniGitHub.Data;

public class Database : IDisposable {
    private readonly DbConnection _connection;
    
    public Database() {
        _connection = DBConnector.CreateConnection();
        _connection.Open();
    }

    public void Dispose() {
        _connection.Dispose();
    }

    // Access to results.
    public DbDataReader ExecuteReader(string sql, Dictionary<string, object> parameters) {
        DbCommand command = _connection.CreateCommand();
        command.CommandText = sql; 
        foreach ((string key, object value) in parameters) {
            command.AddWithValue(key, value);
        }

        return command.ExecuteReader();
    }

    // Number of rows affected
    public int ExecuteNonQuery(string sql, Dictionary<string, object> parameters) {
        var command = _connection.CreateCommand();
        command.CommandText = sql;
        foreach ((string key, object value) in parameters) {
            command.AddWithValue(key, value);
        }

        return command.ExecuteNonQuery();
    }

    // First column of the first row
    public object? ExecuteScalar(string sql,  Dictionary<string, object> parameters) {
        var command = _connection.CreateCommand();
        command.CommandText = sql;
        foreach ((string key, object value) in parameters) {
            command.AddWithValue(key, value);
        }

        return command.ExecuteScalar();
    }
        

}