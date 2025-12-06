using System.Data.Common;
using MiniGitHub.Data.DataConnector.SqlConnector.Extensions;

namespace MiniGitHub.Data.DAOs.SqlDAOs;

public class SqlDatabaseCall(DbConnection connection) {

    // Access to results.
    public DbDataReader ExecuteReader(string sql, Dictionary<string, object?> parameters) {
        DbCommand command = connection.CreateCommand();
        command.CommandText = sql; 
        
        foreach ((string key, object? value) in parameters) {
            command.AddWithValue(key, value);
        }

        return command.ExecuteReader();
    }

    // Number of rows affected
    public int ExecuteNonQuery(string sql, Dictionary<string, object?> parameters) {
        var command = connection.CreateCommand();
        command.CommandText = sql;
        
        foreach ((string key, object? value) in parameters) {
            command.AddWithValue(key, value);
        }

        return command.ExecuteNonQuery();
    }

    // First column of the first row
    public object? ExecuteScalar(string sql,  Dictionary<string, object?> parameters) {
        var command = connection.CreateCommand();
        command.CommandText = sql;
        
        foreach ((string key, object? value) in parameters) {
            command.AddWithValue(key, value);
        }

        return command.ExecuteScalar();
    }
}