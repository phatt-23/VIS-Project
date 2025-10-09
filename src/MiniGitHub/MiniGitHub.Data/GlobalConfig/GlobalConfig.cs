using Microsoft.Data.SqlClient;
using MiniGitHub.Data.DataConnector;

namespace MiniGitHub.Data;

public static class GlobalConfig {

    public static IDataConnector GetDataConnector() {
        if (_dataSource == "sqlite") {
            return new SqlDataConnector(GetSqlConnectionString());
        } 
        if (_dataSource == "text") {
            return new TextDataConnector(GetTextFilePath());
        }
        throw new InvalidOperationException("Unknown data source");
    }

    // helper for getting the text file database
    private static string GetTextFilePath() {
        string solutionDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string dbPath = Path.Combine(solutionDir, "MiniGitHubDB.txt");
        return dbPath;
    }

    // helper for creating a connection string 
    private static string GetSqlConnectionString() {
        string solutionDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string dbPath = Path.Combine(solutionDir, "MiniGitHubDB.sqlite3");
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = dbPath;
        return builder.ConnectionString;
    }

    private static readonly string _dataSource = "sqlite";
}