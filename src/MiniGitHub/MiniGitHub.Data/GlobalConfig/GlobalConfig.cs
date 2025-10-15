using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using MiniGitHub.Data.DataConnector;

namespace MiniGitHub.Data;

public enum DataConnectorDataSource {
    Text, 
    Sqlite,
}

public static class GlobalConfig {

    public static IDataConnector GetDataConnector() {
        if (_dataSource == DataConnectorDataSource.Sqlite) {
            return new SqlDataConnector(GetSqlConnectionString());
        } 
        if (_dataSource == DataConnectorDataSource.Text) {
            return new TextDataConnector(GetTextFilePath());
        }
        throw new InvalidOperationException("Unknown data source");
    }

    public static void SetDataSource(DataConnectorDataSource source) {
        _dataSource = source;
    }

    // helper for getting the text file database
    private static string GetTextFilePath() {
        string solutionDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string dbPath = Path.Combine(solutionDir, _textDbDirectory);
        return dbPath;
    }

    // helper for creating a connection string 
    private static string GetSqlConnectionString() {
        string solutionDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        string dbPath = Path.Combine(solutionDir, _sqliteFile);
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = dbPath;
        return builder.ConnectionString;
    }

    // private static readonly string _dataSource = "sqlite";
    private static DataConnectorDataSource _dataSource = DataConnectorDataSource.Text;
    private static readonly string _sqliteFile = "MiniGitHubDB.sqlite3";
    private static readonly string _textDbDirectory = "MiniGitHubDB.txt.data/";
    
}